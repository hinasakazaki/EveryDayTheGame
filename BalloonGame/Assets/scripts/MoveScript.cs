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

    private KeyCode up = KeyCode.UpArrow, down = KeyCode.DownArrow, jump = KeyCode.Space, enter = KeyCode.Return, interact = KeyCode.X, left = KeyCode.LeftArrow, right = KeyCode.RightArrow;
    public void OnKeybindingChanged(object sender, Dictionary<OptionsScript.Actions, KeyCode> keybindings)
    {
        up = keybindings[OptionsScript.Actions.UP];
        down = keybindings[OptionsScript.Actions.DOWN];
        left = keybindings[OptionsScript.Actions.LEFT];
        right = keybindings[OptionsScript.Actions.RIGHT];
        jump = keybindings[OptionsScript.Actions.JUMP];
        interact = keybindings[OptionsScript.Actions.INTERACT];
        enter = keybindings[OptionsScript.Actions.CONTINUE];
    }

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

        if (Input.GetKey(left))//left and right are for player character
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
        if (Input.GetKey(right)) 
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
        if (Input.GetKeyDown(jump) && grounded)
        {
            healer.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 4), ForceMode2D.Impulse);
        }
        if (Input.GetKey(up) && balloon.transform.localPosition.y < 7) //up and down for NPC character -49.95
        {
            if (attached) balloon.transform.position += Vector3.up * balloonSpeed * Time.deltaTime;
        }
        if (Input.GetKey(down) && balloon.transform.localPosition.y > -3)
        {
            if (attached) balloon.transform.position += Vector3.down * balloonSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(interact))
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
        if (!Input.GetKey(left))
        {
                healerAnim.SetBool("walk_left", false);
        }
        if (!Input.GetKey(right))
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
