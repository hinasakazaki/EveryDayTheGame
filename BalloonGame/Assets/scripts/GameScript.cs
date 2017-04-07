using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    public Slider HealthSlider;
    public GameObject DialogObject;
    public GameObject Audio;
    public GameObject[] levelBGs;
    public GameObject player;
    public GameObject balloon;
    public GameObject Endings;
    public Slider CatSlider;
    public Image damageImage;
    public GameObject sun;

    private GameObject gameUI;
    private int catCount = 40;
    private bool[] catArray = new bool[40];

    private static int health;
    private string[] events = {"Heal1", "Grab", "ScrollStart"};
    private static int eventCounter = 0;
    private int currLevel = 0;

    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    Vector3 firstLocation;

    public enum EndingID
    {
        BAD_END,
        NEUTRAL_END,
        GOOD_END
    }

    // Use this for initialization
    void Start() {
        health = 1;
        DialogObject.SetActive(true);
        gameUI = CatSlider.transform.parent.gameObject;
        player = balloon.transform.parent.gameObject;
        firstLocation = player.transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (sun.activeInHierarchy)
        {
            sun.transform.Rotate(0, 0, 0.2f);
        }
        HealthSlider.value = health;

        if (health <= 0)
        {
            TriggerEnding(EndingID.BAD_END);
        }

        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
    }

    private void TriggerEnding(EndingID ending)
    {
        //remove everything in the game
        gameUI.SetActive(false);
        player.SetActive(false);
        HealthSlider.enabled = false;
        levelBGs[currLevel].SetActive(false);

        switch (ending)
        {
            case EndingID.BAD_END:
                Endings.GetComponent<EndingScript>().BadEnd();
                Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.DEPARTING_SOULS);
                break;
            case EndingID.GOOD_END:
                Endings.GetComponent<EndingScript>().HappyEnd();
                Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.MOMENT_OF_JOY);
                break;
            default:
                Endings.GetComponent<EndingScript>().NeutralEnd();

                break;
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
                TriggerStartScroll();
                break;
        }
    }

    //trigger events
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

    private void TriggerStartScroll()
    {
        levelBGs[0].GetComponent<SideScrollingScript>().StartScroll();
    }

    //complete events

    public void CompletedEvent1()
    {
        DialogObject.GetComponent<DialogScript>().GetOutOfPause();
        eventCounter += 1;
    }

    public void CompletedEvent0()
    {
        DialogObject.GetComponent<DialogScript>().GetOutOfPause();
        eventCounter += 1;
        Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.PONDERING2);
    }

    //general events
    public void Healing(GameObject mush)
    {
        mush.GetComponent<ShroomScript>().getConsumed();
        Audio.GetComponent<AudioManager>().playSFX(AudioManager.SFXList.HEAL);
        TakeHealOrDamage(100);

        if (eventCounter == 0) { CompletedEvent0();  }
    }

    public void TakeHealOrDamage(int x)
    {
        health += x;
        if (x < 0)
        {
            Debug.Log("Taking damage in GameScript" + x);
            Audio.GetComponent<AudioManager>().playSFX(AudioManager.SFXList.DAMAGE);
            damageImage.color = flashColour;
        }
    }

    public void LoadNewLevel()
    {
        if (currLevel == 0)
        {
            sun.SetActive(false);
        }
        levelBGs[currLevel].SetActive(false);
        currLevel += 1;
        levelBGs[currLevel].SetActive(true);

        //set player back to usual location
        player.transform.position = firstLocation;
    }

    public void OnCatExorcised(int i)
    {
        if (!catArray[i])
        {
            catArray[i] = true;
            CatSlider.value += 1;
            Audio.GetComponent<AudioManager>().playSFX((AudioManager.SFXList)Random.Range((int)AudioManager.SFXList.CUTEMEOW1, (int)AudioManager.SFXList.CUTEMEOW6));
        }
    }
}
