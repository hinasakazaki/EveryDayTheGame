using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour {
    
    public GameObject healerDead;
    public GameObject heroDead;
    public GameObject neutralEnd;
    public GameObject happyEnd;
    public GameObject happyEndGame;
    public GameObject happyEndUI;
    public Text happyText;
    public Image fade;

    public Text catTrain;
    public Text tentacleCount;
    public Text healthAbove95;
    public GameObject reportCard; //maybe fade in?


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
        catTrain.text = catTrain.text.Replace("0", GameScript.longestTrain.ToString());
        tentacleCount.text = tentacleCount.text.Replace("0", GameScript.tentacleTrapped.ToString());
        float total = (GameScript.above95 + GameScript.below95);
        Debug.Log("above95" + GameScript.above95 + "total" + total);

        float decimalOfAbove95 = (GameScript.above95 / total);
        Debug.Log("Decimal" + decimalOfAbove95);
        int percent = (int)(decimalOfAbove95 * 100);
        Debug.Log("percent" + percent);
        healthAbove95.text = healthAbove95.text.Replace("0", percent.ToString());

        //  happyEnd.SetActive(true);
        happyEndGame.SetActive(true);
        happyEndUI.SetActive(true);
        happyText.text = happyText.text.Replace("X", numCats.ToString());

        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        yield return new WaitForSeconds(5);
        fade.CrossFadeAlpha(1, 5f, false);
    }
}
