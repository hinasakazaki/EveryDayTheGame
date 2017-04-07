using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour {

    public GameObject badEnd;
    public GameObject neutralEnd;
    public GameObject happyEnd;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BadEnd()
    {
        badEnd.SetActive(true);
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
