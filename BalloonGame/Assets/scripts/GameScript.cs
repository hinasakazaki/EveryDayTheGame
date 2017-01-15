using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    public Slider HealthSlider;
    public GameObject DialogObject;
    public GameObject Audio;

    private static int health;
    private string[] events = {"Heal1", "Grab", "ScrollStart"};
    private static int eventCounter = 0;

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
                break;
            default:
                break;
        }
        eventCounter += 1;
    }

    private void TriggerHealEvent0()
    {
        //here, we make the mushroom appear and stuff
    }

    public void CompletedEvent0()
    {
        
        DialogObject.GetComponent<DialogScript>().GetOutOfPause();
    }

    //general events
    public void Healing(GameObject mush)
    {
        Debug.Log(AudioManager.SFXList.HEAL);

        mush.GetComponent<ShroomScript>().getConsumed();
        Audio.GetComponent<AudioManager>().playSFX(AudioManager.SFXList.HEAL);
        Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.MOMENT_OF_JOY);
        TakeHealOrDamage(100);
    }

    public void TakeHealOrDamage(int x)
    {
        health += x;
    }

    public void LoadNewLevel(int level)
    {

    }
}
