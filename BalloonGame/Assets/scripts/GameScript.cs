using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {
    //static variables
    public static bool ended;
    public static int currLevel;
    public static float longestTrain;
    public static int above95;
    public static int below95;
    public static int tentacleTrapped;
    public static bool nekolordExorcised;

    private static int health;
    private static int eventCounter = 0;

    public Slider HealthSlider;
    public GameObject DialogObject;
    public GameObject Audio;
    public GameObject[] levelBGs;
    public GameObject player;
    public GameObject balloon;
    public GameObject Endings;
    public Slider CatSlider;
    public Slider BossSlider;
    public Image damageImage;
    public GameObject sun;
    public GameObject gameUI;
    public Text titleText;
    public GameObject snow;
    public GameObject tutorialShroom;
    public GameObject tutorialTextBalloon;

    public bool DuringDialog { private set; get; }

    private string PlayerName;
    private int NekoLordIndex = 400;
    private int catCount = 26;
    private bool[] catArray = new bool[40];
    private bool healerDied = false;

    private string[] events = {"Heal1", "Grab", "ScrollStart"};
    private string[] titles = { "Level 1: Morning Clouds", "Level 2: Tiled Prison", "Level 3: Setting Sun", "Level 4: Snowy Darkness", "Level 5: Home" };

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
        ended = false;
        healerDied = false;
        DuringDialog = true;

        health = 1;
        DialogObject.SetActive(true);
        player = balloon.transform.parent.gameObject;
        firstLocation = player.transform.position;

        if (currLevel != 0)
        {
            levelBGs[0].SetActive(false);
            sun.SetActive(false);
            currLevel -= 1;
            LoadNewLevel(true);
        }
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

        if (!ended)
        {
            if (health >= 95)
            {
                above95 += 1;
            }
            else
            {
                below95 += 1;
            }
        }
    }

    public void SetName(string pn)
    {
        this.PlayerName = pn;
    }

    public void OnTentacleCollided()
    {
        Audio.GetComponent<AudioManager>().playSFX(AudioManager.SFXList.TENTACLE);
        healerDied = true;
        TriggerEnding(EndingID.BAD_END);
    }

    private void RemoveBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }
    }

    public void TriggerEnding(EndingID ending)
    {
        if (!ended)
        {
            ended = true;

            //save stats
            if (longestTrain < CatSlider.value)
            {
                longestTrain = CatSlider.value;
            }
            

            //remove everything in the game
            gameUI.SetActive(false);
            player.SetActive(false);
            sun.SetActive(false);
            HealthSlider.enabled = false;
            levelBGs[currLevel].SetActive(false);
            GameObject replay = titleText.gameObject.transform.FindChild("ReplayText").gameObject;

            RemoveBullets();

            switch (ending)
            {
                case EndingID.BAD_END:
                    Endings.GetComponent<EndingScript>().BadEnd(healerDied);
                    if (healerDied)
                    {
                        tentacleTrapped += 1;
                    }
                    if (currLevel == 4)
                    {
                        Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.DEPARTING_SOULS_TROMBONE);
                    }
                    else
                    {
                        Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.DEPARTING_SOULS);
                    }
                    snow.SetActive(true);

                    titleText.color = new Color32(0x9F, 0x1B, 0x1E, 0xFF);
                    string tempPlayerName = (PlayerName == null || PlayerName.Length == 0) ? "You were" : PlayerName + " was";
                    titleText.text = healerDied ? tempPlayerName + " trapped forever. \nThe end." : "The hero died. \nThe end.";
                         
                    titleText.gameObject.SetActive(true);

                    replay.GetComponent<Text>().color = new Color32(0x9F, 0x1B, 0x1E, 0xFF);

                    //9F1B1EFF
                    break;
                case EndingID.GOOD_END:

                    sun.SetActive(true);
                    Endings.GetComponent<EndingScript>().HappyEnd((int)CatSlider.value);

                    titleText.text = "";
                    titleText.gameObject.SetActive(true);

                    replay.GetComponent<Text>().color = Color.white;
                    replay.SetActive(true);
                    break;
                default:
                    Endings.GetComponent<EndingScript>().NeutralEnd();
                    Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.PONDERING3);

                    titleText.color = Color.white;
                    titleText.text = "The two went on separate ways. \nThe end.";
                    titleText.gameObject.SetActive(true);
                    replay.GetComponent<Text>().color = Color.white;

                    break;
            }
            
            StartCoroutine(ShowRestartOption(replay));
        }
    }

    private bool restartOptionAvailable;

    IEnumerator ShowRestartOption(GameObject restartOption)
    {
        yield return new WaitForSeconds(5);
        restartOption.SetActive(true);
        restartOptionAvailable = true;
    }
   
    IEnumerator SetTutorialTextInactive()
    {
        yield return new WaitForSeconds(5);
        tutorialTextBalloon.SetActive(false);
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
            case 2:
                Debug.Log("eventCounter 2");
                this.DuringDialog = false;
                titleText.color = Color.white;
                titleText.text = titles[currLevel];
                titleText.gameObject.SetActive(true);
                TriggerStartScroll();
                StartCoroutine(fadeTitleText());
                break;
            case 3:
                Debug.Log("eventCounter 3");
                this.DuringDialog = false;
                titleText.color = Color.white;
                titleText.text = titles[currLevel];
                titleText.gameObject.SetActive(true);
                TriggerStartScroll();
                StartCoroutine(fadeTitleText());
                break;
            case 4:
                Debug.Log("EventCounter 4 - nekolord exorcised");
                TriggerStartScroll(true);
                Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.AT_HOME);
                DialogObject.GetComponent<DialogScript>().GetOutOfPause();
                DuringDialog = true;
                eventCounter += 1;
                RemoveBullets();
                break;
            default:
                break;
        }

        if (ended == true && restartOptionAvailable)
        {
            eventCounter = 0;
            health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator fadeTitleText()
    {
        yield return new WaitForSeconds(5f);
        if (!ended)
        {
            titleText.gameObject.SetActive(false);
        }
    }

    public void TriggerJumpDown()
    {
        balloon.GetComponent<BalloonScript>().TriggerJumpDown();
    }

    public void TriggerUnmask()
    {
        Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.MOMENT_OF_JOY);
        balloon.GetComponent<BalloonScript>().TriggerUnmask();
    }

    //trigger events
    private void TriggerHealEvent0()
    {
        DuringDialog = false;

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
        DuringDialog = false;

        balloon.GetComponent<BalloonScript>().StartGrabTutorial();
    }

    private void TriggerStartScroll(bool stop = false)
    {
        DuringDialog = false;

        Debug.Log("currlevel" + currLevel);
        Debug.Log("currlevel" + levelBGs[currLevel]);
        levelBGs[currLevel].GetComponent<SideScrollingScript>().StartScroll();

        if (stop)
        {
            levelBGs[currLevel].GetComponent<SideScrollingScript>().StopScroll();
        }
    }

    //complete events
    public void CompletedEvent0()
    {
        DuringDialog = true;
        DialogObject.GetComponent<DialogScript>().GetOutOfPause();
        eventCounter += 1;
        Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.PONDERING2);
    }

    public void CompletedEvent1()
    {
        DuringDialog = true;
        DialogObject.GetComponent<DialogScript>().GetOutOfPause();
        eventCounter += 1;

        tutorialTextBalloon.SetActive(true);
        StartCoroutine(SetTutorialTextInactive());
    }

    //general events
    public void Healing(GameObject mush)
    {
        mush.GetComponent<ShroomScript>().getConsumed();
        Audio.GetComponent<AudioManager>().playSFX(AudioManager.SFXList.HEAL);
        TakeHealOrDamage(25);
        
        if (eventCounter == 0)
        {
            balloon.GetComponent<BalloonScript>().heroHealed();
            CompletedEvent0();
        }
    }

    public void TakeHealOrDamage(int x)
    {
        if (eventCounter >= 5)
        {
            return;
        }

        health += x;
        if (health > 100)
        {
            health = 100;
        }
        if (x < 0)
        {
            Debug.Log("Taking damage in GameScript" + x);
            Audio.GetComponent<AudioManager>().playSFX(AudioManager.SFXList.DAMAGE);
            damageImage.color = flashColour;
        }

        balloon.transform.GetChild(0).gameObject.GetComponent<HeroScript>().UpdateFrequency(health);
    }

    public void LoadNewLevel(bool fromRestart = false)
    {
        DuringDialog = true;
        switch (currLevel)
        {
            case 0: //going to soda
                sun.SetActive(false);
                Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.PONDERING1);
                break;
            case 1: //going to japan
                Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.PONDERING3);
                break;
            case 2: //going to MN
                snow.SetActive(true);
                Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.PONDERING_SNOW);
                break;
            case 3: //going to home
                snow.SetActive(false);
                Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.RISKS);
                BossSlider.transform.parent.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        
        levelBGs[currLevel].SetActive(false);
        currLevel += 1;
        levelBGs[currLevel].SetActive(true);

        //set player back to usual location
        player.transform.position = firstLocation;
        
        if (!fromRestart)
        {
            DialogObject.GetComponent<DialogScript>().GetOutOfPause();
        }
    }

    public void OnBossDamaged()
    {
        BossSlider.value -= 5;
    }

    public void OnCatExorcised(int i)
    {
        if (i != NekoLordIndex && !catArray[i])
        {
            catArray[i] = true;
            CatSlider.value += 1;
            Audio.GetComponent<AudioManager>().playSFX((AudioManager.SFXList)Random.Range((int)AudioManager.SFXList.CUTEMEOW1, (int)AudioManager.SFXList.CUTEMEOW6));
        }
        else if (i == NekoLordIndex)
        {
            nekolordExorcised = true;
            eventCounter += 1;
            TriggerEvent();
        }
    }

    public void SimuateStartSequence()
    {
        if (currLevel == 0)
        {
            tutorialShroom.GetComponent<ShroomScript>().getConsumed();
            Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.PONDERING2);
        }
        balloon.GetComponent<BalloonScript>().heroHealed();
        TakeHealOrDamage(100);
        eventCounter += 1;

        DuringDialog = true;
        eventCounter += 1; 
 
        DuringDialog = true;
        eventCounter += 1;

        player.GetComponent<MoveScript>().ChangeAttached();
    }
}
