using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomScript : MonoBehaviour {

    public GameObject tutorialText;

    private GameObject sparkles;
    private bool sparkling;
    private bool started = false;

	// Use this for initialization
	void Start () {
        sparkles = transform.GetChild(0).gameObject;
        startSparkle();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject randomSparkle = sparkles.transform.GetChild(Random.Range(0, 4)).gameObject;
        if (sparkling && Random.Range(0, 40) == 1)
        {
            if (!randomSparkle.activeInHierarchy)
            {
                randomSparkle.SetActive(true);
            }
        }
        else if (!sparkling && started)
        {
            if (Random.Range(0, 40) == 1 && randomSparkle.activeInHierarchy)
            {
                randomSparkle.SetActive(false);

                foreach (Transform child in sparkles.transform)
                {
                    if (child.gameObject.activeInHierarchy) return;
                }
                //if all are not active
                sparkles.SetActive(false);
                this.gameObject.SetActive(false);
            }
        }
	}

    public void startSparkle()
    {
        started = true;
        sparkles.SetActive(true);
        sparkling = true;
    }

    public void getConsumed()
    {
        tutorialText.SetActive(false);
        sparkling = false;
    }

    
}
