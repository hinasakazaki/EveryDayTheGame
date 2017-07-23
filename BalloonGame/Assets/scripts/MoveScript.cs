using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
    public GameScript gameScript;
    public bool grounded;

    protected Animator animator;
    float speed = 1.5f;
    float balloonSpeed = 1.0f;

    private GameObject balloon;
    private GameObject healer;
    private GameObject hero;
    private bool attached;
    private bool inCollisionWithShroom;
    private bool inCollisionWithPost;
    private GameObject currMush;

    private Animator healerAnim;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        grounded = true;
        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "Balloon")
            {
                balloon = child.gameObject;
                hero = balloon.transform.GetChild(0).gameObject; //Warning, have to update if more kids
            } else if (child.name == "PlayerSprite")
            {
                healer = child.gameObject;
            }
        }

        healerAnim = healer.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //this is for every frame
        //if in dialog, cant move
        if (gameScript.DuringDialog)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("LeftJoystickX") > 0)//left and right are for player character
        {
            healerAnim.SetBool("walk_left", true);
            if (healer.gameObject.transform.localPosition.x > 2.88 || (attached && transform.localPosition.x < -93))// out of bounds
            {
                return;
            }
            if (!attached)
            {
                healer.transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }

        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("LeftJoystickX") < 0) // figure out joystick
        {
            healerAnim.SetBool("walk_right", true);
            if (healer.gameObject.transform.localPosition.x < -18.5 || (attached && transform.localPosition.x > -77))// bounds
            {
                return;
            }
            if (!attached)
            {
                healer.transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            healer.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.UpArrow)) //up and down for NPC character
        {
            if (attached) balloon.transform.position += Vector3.up * balloonSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (attached) balloon.transform.position += Vector3.down * balloonSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (inCollisionWithShroom && currMush != null)
            {
                gameScript.Healing(currMush);
                balloon.GetComponent<BalloonScript>().heroHealed();
                currMush = null;
            }
            else if (inCollisionWithPost)
            {
                attached = true;
                gameScript.CompletedEvent1();
                balloon.GetComponent<BalloonScript>().ChangeAttached(healer);
            }
        }
        if (!Input.GetKey(KeyCode.LeftArrow))
        {
                healerAnim.SetBool("walk_left", false);
        }
        if (!Input.GetKey(KeyCode.RightArrow))
        {
            healerAnim.SetBool("walk_right", false);
        }
    }

    public void OnSchroomCollisionEntered(GameObject mush)
    {
        currMush = mush;
        inCollisionWithShroom = true;
    }

    public void OnTentacleCollisionEntered(GameObject tentacle)
    {
        gameScript.OnTentacleCollided();
    }

    public void OnSchroomCollisionExited()
    {
        inCollisionWithShroom = false;
        currMush = null;
    }

    public void OnPostCollisionEntered()
    {
        inCollisionWithPost = true;
    }

    public void OnDoorCollisionEntered()
    {
        Debug.Log("Door collision");
        gameScript.LoadNewLevel();
       
    }

    public void OnPostCollisionExited()
    {
        inCollisionWithPost = false;
    }

    public void TakeDamage(int x)
    {
        Debug.Log("Take damage from MOve Script" + x);
        gameScript.TakeHealOrDamage(-x);
    }

}
