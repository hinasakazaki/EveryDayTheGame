using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour {

    public Text DialogName;
    public Text DialogBody;
    public GameObject BGDialog;
    public GameScript gameScript;

    DialogObject[] dialogObject;
    int counter = 0;
    int optionCounter = 0;
    DialogObject curDialog;
    private string playerName;
    private string playerHome;

    private bool duringDecision = false;
    private bool duringInput = false;
    private string inputString = "";

    // Use this for initialization
    void Start () {
        dialogObject = InitializeDialog();
        SwitchDialog();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void GetOutOfPause()
    {
        counter = curDialog.next[0];
        curDialog.pauseAfter = false; //because fulfilled
        BGDialog.SetActive(true);
        SwitchDialog();
    }

    public void SwitchDialog()
    {
        switch (counter) //-1 = bad ending, -2 = neutral end, -3 = good end, -4=halt dialog 0 indexed dialog
        {
            case -1:
                gameScript.TriggerEnding(GameScript.EndingID.BAD_END);
                break;
            case -2:
                gameScript.TriggerEnding(GameScript.EndingID.NEUTRAL_END);
                break;
            case -3:
                gameScript.TriggerEnding(GameScript.EndingID.GOOD_END);
                break;

            default:
                break;
        }

        if (curDialog != null && curDialog.pauseAfter == true)
        {
            BGDialog.SetActive(false);
            gameScript.TriggerEvent();
            return;
        }

        curDialog = dialogObject[counter];
       
        DialogName.text = (curDialog.speaker == "PlayerName") ? playerName : curDialog.speaker;

        if (curDialog.options == null) { 
            if (curDialog.dialog == "InputField")
            {
                DialogBody.text = "Start typing..";
                DialogBody.color = Color.gray;
                duringInput = true;
                inputString = "";
            } 
            else
            {
                string newDialog = curDialog.dialog.Replace("[playerName]", playerName);
                newDialog = newDialog.Replace("[playerHome]", playerHome);
                DialogBody.text = newDialog;
            }
        }
        else //there are options!!
        {
            optionCounter = 0;
            updateOptionTextWithChecks();
            duringDecision = true;
        }
    }

    private void updateOptionTextWithChecks()
    {

        checkOptionBounds();

        string optionsString = "";
        for (int i = 0; i < curDialog.options.Length; i++)
        {
            if (i == optionCounter)
            {
                optionsString += curDialog.options[i] + " < \n";
            }
            else
            {
                optionsString += curDialog.options[i] + "\n";
            }
        }

        DialogBody.text = optionsString;
    }

    private void checkOptionBounds()
    {
        if (optionCounter < 0)
        {
            optionCounter = 0;
        }
        else if (optionCounter > curDialog.options.Length - 1)
        {
            optionCounter = curDialog.options.Length - 1;
        }
    }

    public void OnInput(string action)
    {
        if (duringDecision)
        {
            if (action == "Down")
            {
                optionCounter += 1;
            }
            else if (action == "Up")
            {
                optionCounter -= 1;
            }
            updateOptionTextWithChecks();

            if (action == "Enter")
            {
                duringDecision = false;
                counter = curDialog.next[optionCounter];
                SwitchDialog();
            }
        }
        else if (duringInput)
        {
            if (action == "Delete")
            {
                inputString = inputString.Substring(0, inputString.Length - 1);
                DialogBody.text = inputString;
            }
            else if (action == "Enter")
            {
                duringInput = false;
                if (counter == 4) { playerName = inputString; }
                else if (counter == 7) { counter = curDialog.next[0]; playerHome = inputString;  } //?>>
                counter = curDialog.next[0];
                SwitchDialog();
            }
            else if (action != "Up" && action != "Down") //have to black list all of the possible inputs.. yay...
            {
                DialogBody.color = Color.black;
                inputString += action;
                DialogBody.text = inputString;
            }
        }
        else
        {
            if (action == "Enter")
            {
                counter = curDialog.next[0];
                SwitchDialog();
            }
        }
    }

    private DialogObject[] InitializeDialog()
    {
        DialogObject[] dialogObjects = { new DialogObject("hero", "Halp... I'm dying....", null, new int[] {1 }, false), //0
                                         new DialogObject("You", null, new string[] {"Use mushroom healing powers to save a life", "Ignore" }, new int[] {2, -1}, true),
                                         new DialogObject("hero", "Thank you, I feel much better now.", null, new int[] {3}, false),
                                         new DialogObject("hero", "What's your name? What are you doing here?", null, new int[] {4}, false),
                                         new DialogObject("Enter  Name", "InputField", null, new int[] {5}, false),
                                         new DialogObject("PlayerName", "My name is [playerName]. I'm not sure how I ended up here..", null, new int[] {6}, false), //5
                                         new DialogObject("hero", "Okay, [playerName], where are you from?", null, new int[] {7}, false),
                                         new DialogObject("Enter  Home", "InputField", null, new int[] {8}, false),
                                         new DialogObject("hero", "That's where the evil neko-lord lives.", null, new int[] {9}, false),
                                         new DialogObject("hero", "You see, I've been freeing kittens from evil brainwashing by the neko-lord.", null, new int[] {10}, false),
                                         new DialogObject("hero", "I've come this far, but the kitten who was helping me accidentally got brainwashed by another kitten and she attacked me.", null, new int[] {11}, false),
                                         new DialogObject("hero", "They're over there right now.", null, new int[] {12}, false),
                                         new DialogObject("hero", "Would you like to come with me to [playerHome]? There's lots of kittens to save, and I could use a mushroom healer.", null, new int[] {13}, false),
                                         new DialogObject("PlayerName", null, new string[] {"Sure", "No Thanks"}, new int[] {14, 15}, false),
                                         new DialogObject("hero", "Sounds great. Take a hold of the balloon there.", null, new int[] {16}, true), //14 -- here we make sure balloon is caught
                                         new DialogObject("hero", "All right, have a good life!", null, new int[] {-2}, true), //15 leads to neutral ending
                                         new DialogObject("hero", "Nice, let's get going.", null, new int[] {17}, false),
                                         new DialogObject("PlayerName", null, new string[] {"Ok", "Hold on"}, new int[] {18, 16}, true), //17, where scroll is halted

                                         //Level 2
                                         new DialogObject("hero", "What a strange place. Shall we keep going?", null, new int[] {19}, false), //18
                                         new DialogObject("PlayerName", null, new string[] {"Ok", "Hold on"}, new int[] {20, 18}, true), //19, where scroll is halted

                                         //Level 3
                                         new DialogObject("hero", "Sun's starting to set, we better hurry.", null, new int[] {21}, false), //20
                                         new DialogObject("PlayerName", null, new string[] {"Ok", "Hold on"}, new int[] {22, 20}, true),

                                         //Level 4
                                         new DialogObject("hero", "Snow! Snow! Let's go!", null, new int[] {23}, false), //22
                                         new DialogObject("PlayerName", null, new string[] {"Ok", "Hold on"}, new int[] {22, 24}, true),

                                         //Level 5
                                         new DialogObject("hero", "This is it. [playerhome].", null, new int[] {25}, false), //24
                                         new DialogObject("PlayerName", null, new string[] {"Let's go.", "Wait up."}, new int[] {24, 26}, true),

                                        new DialogObject("neko 1ord", "Hiss!!! You will never defeat me!", null, new int[] {27}, false),

                                        new DialogObject("neko 1ord", "meow~ <3", null, new int[] {28}, false),

                                        new DialogObject("hero", "Well, I think that was it.", null, new int[] {29}, false),

                                        new DialogObject("cats", "Thank you brave hero for freeing us!", null, new int[] {30}, false),
                                        new DialogObject("hero", "Thank my friend here, PlayerName.", null, new int[] {31}, false),
                                        new DialogObject("hero", "Well...", null, new int[] {32}, false),
                                        new DialogObject("hero", "Actually, I haven't known you for just today. I've known you for a long time.", null, new int[] {33}, false),
                                        new DialogObject("hero", "In fact, I know your name isn’t PlayerName, it's Chris.", null, new int[] {34}, false),
                                        new DialogObject("hero", "And my name is Hina.", null, new int[] {35}, false),
                                        new DialogObject("hina", "You've kept me safe and alive through our journey today, and I'm so thankful.", null, new int[] {36}, false),
                                        new DialogObject("hina", "I hope we can be together for the rest of our lives..", null, new int[] {37}, false),
                                        new DialogObject("hina", "Will you marry me?", null, new int[] {38}, false),
                                        new DialogObject("chris", null, new string[] {"Sure.", "Not now."}, new int[] {39, 40}, true),
                                        new DialogObject("hina", "Good choice.", null, new int[] {-3}, false), //39
                                        new DialogObject("hina", "That's cool. Thank you for helping me today.", null, new int[] {-2}, false), //goes to neutral end
        };
        return dialogObjects;
    }
}

public class DialogObject
{
    public readonly string speaker;
    public readonly string dialog;
    public string[] options;
    public bool pauseAfter;
    public readonly int[] next; 

    public DialogObject(string speaker, string dialog, string[] options, int[] next, bool pause)
    {
        this.speaker = speaker;
        this.dialog = dialog;
        this.options = options;
        this.next = next;
        this.pauseAfter = pause;
    }


}
