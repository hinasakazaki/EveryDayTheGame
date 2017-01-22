using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    public Slider HealthSlider;
    public GameObject DialogObject;
    public GameObject Audio;
    public GameObject[] levelBGs;
    public GameObject balloon;

    private static int health;
    private string[] events = {"Heal1", "Grab", "ScrollStart"};
    private static int eventCounter = 0;
    private int currLevel = 0;

    // Use this for initialization
    void Start() {
        health = 0;
        DialogObject.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        HealthSlider.value = health;

        if (health <= 0)
        {
            //game end
        }


    }

    public void TriggerEvent()
    {
        switch (eventCounter)
        {
            case 0:
                TriggerHealEvent0();
                break;
            case 1:
                TriggerGrabEvent();
                break;
            default:
                break;
        }
    }

    private void TriggerHealEvent0()
    {
        //here, we make the mushroom tutorial to come up
        foreach (Transform child in levelBGs[0].transform)
        {
            if (child.name == "tutorialShroom")
            {
                child.GetComponent<ShroomScript>().startSparkle(true);
            }
        }
    }

    private void TriggerGrabEvent()
    {
        balloon.GetComponent<BalloonScript>().StartGrabTutorial();
    }

    public void CompletedEvent1()
    {
        DialogObject.GetComponent<DialogScript>().GetOutOfPause();
        eventCounter += 1;
    }

    public void CompletedEvent0()
    {
        DialogObject.GetComponent<DialogScript>().GetOutOfPause();
        eventCounter += 1;
    }

    //general events
    public void Healing(GameObject mush)
    {
        mush.GetComponent<ShroomScript>().getConsumed();
        Audio.GetComponent<AudioManager>().playSFX(AudioManager.SFXList.HEAL);
        Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.MOMENT_OF_JOY);
        TakeHealOrDamage(100);

        if (eventCounter == 0) { CompletedEvent0();  }
    }

    public void TakeHealOrDamage(int x)
    {
        health += x;
    }

    public void LoadNewLevel()
    {
        levelBGs[currLevel].SetActive(false);
        currLevel += 1;
        levelBGs[currLevel].SetActive(true);
    }
}
