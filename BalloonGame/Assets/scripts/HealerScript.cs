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
        else if (collisionName.Contains("Door"))
        {
            Player.GetComponent<MoveScript>().OnDoorCollisionEntered();

        }
        else if (collisionName.Contains("ground"))
        {
            Player.GetComponent<MoveScript>().grounded = true;
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
        else if (collisionName.Contains("ground"))
        {
            Player.GetComponent<MoveScript>().grounded = false;
        }
    }
}
