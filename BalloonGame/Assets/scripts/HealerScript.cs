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
        Debug.Log("COLLIDN");
        if (collision.gameObject.name.Contains("Shroom"))
        {
            Player.GetComponent<MoveScript>().OnSchroomCollisionEntered(collision.gameObject);
        }

        else if (collision.gameObject.name.Contains("post"))
        { 
            Debug.Log("HELL2 O");
            Player.GetComponent<MoveScript>().OnPostCollisionEntered();
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
            Debug.Log("HELLO");
            Player.GetComponent<MoveScript>().OnPostCollisionExited();
        }
    }
}
