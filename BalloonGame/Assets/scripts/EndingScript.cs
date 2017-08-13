using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour {

    public GameObject healerDead;
    public GameObject heroDead;
    public GameObject neutralEnd;
    public GameObject happyEnd;
    public GameObject happyText;
    public Image fade;

	// Use this for initialization
	void Start () {
        fade.canvasRenderer.SetAlpha(0.0f);
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

    public void HappyEnd(int numCats)
    {
        happyEnd.SetActive(true);
        happyText.SetActive(true);
        happyText.GetComponentInChildren<Text>().text = string.Format("The happy pair lived in good health and cheer for many a long and prosperous days with their {0} rescued cats.\nThe end.", numCats.ToString());
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        yield return new WaitForSeconds(5);
        fade.CrossFadeAlpha(1, 5f, false);
    }
}
