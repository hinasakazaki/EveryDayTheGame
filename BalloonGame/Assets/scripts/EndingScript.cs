using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour {

    public GameObject healerDead;
    public GameObject heroDead;
    public GameObject neutralEnd;
    public GameObject happyEnd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BadEnd(bool healerDied)
    {
        if (healerDied)
        {
            healerDead.SetActive(true);
            heroDead.SetActive(false);

        }
        else
        {
            heroDead.SetActive(true);
            healerDead.SetActive(false);
        }
    }

    public void NeutralEnd()
    {
        neutralEnd.SetActive(true);
    }

    public void HappyEnd()
    {
        happyEnd.SetActive(true);
    }
}
