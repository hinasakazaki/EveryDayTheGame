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
    public static string playerName;
    public static string playerHome;

    private bool duringDecision = false;
    private bool duringInput = false;
    private string inputString = "";
    private Color chooseColor = new Color(115f / 255.0f, 139f / 255.0f, 155f / 255.0f);
    // Use this for initialization
    void Start () {
        dialogObject = InitializeDialog();
       
        if (playerName != null && playerHome != null)
        {
            gameScript.SimuateStartSequence();
            int levelint = 17;
            switch  (GameScript.currLevel)
            {
                case 0:
                    levelint = 17;
                    break;
                case 1:
                    levelint = 19;
                    break;
                case 2:
                    levelint = 21;
                    break;
                case 3:
                    levelint = 23;
                    break;
                case 4:
                    levelint = 25;
                    break;
                default:
                    levelint = 17;
                    break;
            }
            curDialog = new DialogObject("", "", null, new int[] { levelint }, false);
            GetOutOfPause();
        }

        SwitchDialog();
    }
	
    public void Ended()
    {
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
            case 31:
                gameScript.TriggerJumpDown();
                break;
            case 35:
                gameScript.TriggerUnmask();
                break;
            default:
                break;
        }

        if (curDialog != null && curDialog.pauseAfter == true && !(isHalting() && optionCounter == 1))
        {
            BGDialog.SetActive(false);
            gameScript.TriggerEvent();
            return;
        }

        curDialog = dialogObject[counter];
        DialogBody.color = Color.black;

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
        DialogBody.color = chooseColor;
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
            else if (!string.IsNullOrEmpty(inputString) && action == "Enter")
            {
                duringInput = false;
                if (counter == 4) { playerName = inputString; gameScript.SetName(playerName);  }
                else if (counter == 7) { counter = curDialog.next[0]; playerHome = inputString; } 
                counter = curDialog.next[0];
                SwitchDialog();
            }
            else if (action != "Up" && action != "Down" && action != "Enter") //have to black list all of the possible inputs.. yay...
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

    private bool isHalting()
    {
        if (counter == 17 || counter == 19 || counter == 21 || counter == 23 || counter == 26)
        {
            return true;
        }
        return false;
    }

    private DialogObject[] InitializeDialog()
    {
        DialogObject[] dialogObjects = { new DialogObject("hero", "Halp... I'm dying....", null, new int[] {1 }, false), //0
                                         new DialogObject("You", null, new string[] {"Use mushroom healing powers to save a life", "Ignore" }, new int[] {2, -1}, true),
                                         new DialogObject("hero", "Thank you, I feel much better now.", null, new int[] {3}, false),
                                         new DialogObject("hero", "What's your name? What are you doing here?", null, new int[] {4}, false),
                                         new DialogObject("Enter  Name", "InputField", null, new int[] {5}, false),
                                         new DialogObject("PlayerName", "My name is [playerName]. I'm not sure how I ended up here..", null, new int[] {6}, false), //5
                                         new DialogObject("hero", "Okay, [playerName], where is your home?", null, new int[] {7}, false),
                                         new DialogObject("Enter  Home", "InputField", null, new int[] {8}, false),
                                         new DialogObject("hero", "That's where the evil neko-lord lives.", null, new int[] {9}, false),
                                         new DialogObject("hero", "You see, I've been freeing kittens from the neko-lord’s evil brainwashing.", null, new int[] {10}, false),
                                         new DialogObject("hero", "I've come this far, but the kitten who was helping me accidentally got brainwashed by another kitten and she attacked me.", null, new int[] {11}, false),
                                         new DialogObject("hero", "They're over there right now.", null, new int[] {12}, false),
                                         new DialogObject("hero", "Would you like to come with me to [playerHome]? There are lots of kittens to save, and I could use a Mushroom Medic.", null, new int[] {13}, false),
                                         new DialogObject("hero", "I can cure the kittens with my hearts but I'll need you to navigate the balloon and heal me with the mushrooms.", null, new int[] {14}, false), //13
                                         new DialogObject("PlayerName", null, new string[] {"Sure", "No Thanks"}, new int[] {15, 16}, false), //14
                                         new DialogObject("hero", "Sounds great. Take a hold of the balloon there.", null, new int[] {17}, true), //15-- here we make sure balloon is caught
                                         new DialogObject("hero", "All right, have a good life!", null, new int[] {-2}, true), //16 leads to neutral ending
                                         new DialogObject("hero", "Nice, let's get going.", null, new int[] {18}, false), //17
                                         new DialogObject("PlayerName", null, new string[] {"Ok", "Hold on"}, new int[] {19, 17}, true), //18, where scroll is halted

                                         //Level 2
                                         new DialogObject("hero", "What a strange place. Shall we keep going?", null, new int[] {20}, false), //19
                                         new DialogObject("PlayerName", null, new string[] {"Ok", "Hold on"}, new int[] {21, 19}, true), //20, where scroll is halted

                                         //Level 3
                                         new DialogObject("hero", "Sun's starting to set, we better hurry.", null, new int[] {22}, false), //21
                                         new DialogObject("PlayerName", null, new string[] {"Ok", "Hold on"}, new int[] {23, 21}, true),

                                         //Level 4
                                         new DialogObject("hero", "Snow! Snow! Let's go!", null, new int[] {24}, false), //23
                                         new DialogObject("PlayerName", null, new string[] {"Ok", "Hold on"}, new int[] {25, 23}, true),

                                         //Level 5
                                         new DialogObject("hero", "This is it. [playerHome].", null, new int[] {26}, false), //25
                                        new DialogObject("neko 1ord", "They are coming!!! They will never defeat me! My minions, attack!", null, new int[] {27}, false),
                                        new DialogObject("PlayerName", null, new string[] {"Let's go.", "Wait up."}, new int[] {28, 26}, true), //27

                                        new DialogObject("neko 1ord", "meow~ <3", null, new int[] {29}, false),

                                        new DialogObject("hero", "Well, I think that was it.", null, new int[] {30}, false), //29

                                        new DialogObject("cats", "Thank you brave hero for freeing us!", null, new int[] {31}, false),
                                        new DialogObject("hero", "Thank my friend here, [playerName].", null, new int[] {32}, false),
                                        new DialogObject("hero", "Well...", null, new int[] {33}, false),
                                        new DialogObject("hero", "Actually, I haven't known you for just today. I've known you for a long time.", null, new int[] {34}, false),
                                        new DialogObject("hero", "In fact, I know your name isn’t [playerName], it's Chris.", null, new int[] {35}, false),
                                        new DialogObject("hero", "And my name is Hina.", null, new int[] {36}, false),
                                        new DialogObject("hina", "You've kept me safe and alive through our journey today, and I'm so thankful.", null, new int[] {37}, false),
                                        new DialogObject("hina", "You make me the happiest. I hope we can be together for the rest of our lives..", null, new int[] {38}, false),
                                        new DialogObject("hina", "Let's get married.", null, new int[] {39}, false),
                                        new DialogObject("chris", null, new string[] {"Sure.", "No thanks."}, new int[] {40, 41}, false),
                                        new DialogObject("hina", "Good choice.", null, new int[] {-3}, true), //40
                                        new DialogObject("hina", "That's cool. Thank you for helping me today.", null, new int[] {-2}, false), //goes to neutral end
                                        new DialogObject("", "", null, new int[] {-3}, true)
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
