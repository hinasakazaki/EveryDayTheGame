using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
    public GameScript gameScript;

    protected Animator animator;
    float speed = 1.5f;
    float balloonSpeed = 1.0f;

    private GameObject balloon;
    private GameObject healer;
    private bool attached;
    private bool inCollisionWithShroom;
    private bool inCollisionWithPost;
    private GameObject currMush;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "Balloon")
            {
                balloon = child.gameObject;
            } else if (child.name == "PlayerSprite")
            {
                healer = child.gameObject;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("LeftJoystickX") > 0)//left and right are for player character
        {
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
            if (healer.gameObject.transform.localPosition.x < -14.5 || (attached && transform.localPosition.x > -80))// bounds
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
    }

    public void OnSchroomCollisionEntered(GameObject mush)
    {
        currMush = mush;
        inCollisionWithShroom = true;
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

    public void OnPostCollisionExited()
    {
        inCollisionWithPost = false;

    }

}
