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
        Debug.Log("collidn");
        if (collision.gameObject.name.Contains("shroom"))
        {
            Player.GetComponent<MoveScript>().OnSchroomCollisionEntered(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("shroom"))
        {
            Player.GetComponent<MoveScript>().OnSchroomCollisionExited();
        }
    }
}
