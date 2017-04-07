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
        if (collision.gameObject.name.Contains("Shroom"))
        {
            Player.GetComponent<MoveScript>().OnSchroomCollisionEntered(collision.gameObject);
        }

        else if (collision.gameObject.name.Contains("post"))
        {
            Player.GetComponent<MoveScript>().OnPostCollisionEntered();
        }
        else if (collision.gameObject.name.Contains("Door1"))
        {
            Player.GetComponent<MoveScript>().OnDoorCollisionEntered();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Shroom"))
        {
            Player.GetComponent<MoveScript>().OnSchroomCollisionExited();
        }
        else if (collision.gameObject.name.Contains("post"))
        {
            Player.GetComponent<MoveScript>().OnPostCollisionExited();
        }
    }
}
