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
        /**
         * 
                                        new DialogObject("", "The hero and the mushroom healer opened a mushroom soup restaurant x cat cafe together.", null, new int[] {42}, false), //41
                                        new DialogObject("", "The happy pair lived in good health and cheer for many a long and prosperous days.", null, new int[] {42}, false), //42
**/
    }
}
