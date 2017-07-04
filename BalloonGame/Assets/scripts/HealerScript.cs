using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerScript : MonoBehaviour {

    public GameObject Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionName = collision.gameObject.name;

        if (collisionName.Contains("ground"))
        {
            Player.GetComponent<MoveScript>().grounded = true;
        }
        else if (collisionName.Contains("Door"))
        {
            Player.GetComponent<MoveScript>().OnDoorCollisionEntered();

        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        string collisionName = collision.gameObject.name;

        if (collisionName.Contains("ground"))
        {
            Player.GetComponent<MoveScript>().grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionName = collision.gameObject.name;
        if (collisionName.Contains("Shroom"))
        {
            Player.GetComponent<MoveScript>().OnSchroomCollisionEntered(collision.gameObject);
        }

        else if (collisionName.Contains("post"))
        {
            Player.GetComponent<MoveScript>().OnPostCollisionEntered();
        }
      
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        string collisionName = collision.gameObject.name;

        if (collisionName.Contains("Shroom"))
        {
            Player.GetComponent<MoveScript>().OnSchroomCollisionExited();
        }
        else if (collisionName.Contains("post"))
        {
            Player.GetComponent<MoveScript>().OnPostCollisionExited();
        }
    }
}
