using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour {

    public Text DialogName;
    public Text DialogBody;

    DialogObject[] dialogObject;
    int counter = 0;
    int optionCounter = 0;
    DialogObject curDialog;
    private string playerName;

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

    public void SwitchDialog()
    {
        curDialog = dialogObject[counter];

        DialogName.text = (curDialog.speaker == "PlayerName") ? playerName : curDialog.speaker;

        if (curDialog.options == null) { 
            if (curDialog.dialog == "InputField")
            {
                DialogBody.text = "Start typing..";
                DialogBody.color = Color.gray;
                duringInput = true;
                inputString = "";
            } else
            {
                DialogBody.text = curDialog.dialog;
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

    /*public void OnInputFieldChanged(string newText)
    {
        Debug.Log("HJEY");
       
    }
    public void OnButtonClickTest()
    {
        Debug.Log("IT WORKS?");
    }*/

    public void OnInput(string action)
    {
        if (curDialog.options != null)
        {
            Debug.Log("SKDFJKLD");
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
                counter = curDialog.next[optionCounter];
                SwitchDialog();
            }
        }
        else if (duringInput)
        {
            if (action == "Delete")
            {
                inputString = inputString.Substring(0, inputString.Length - 1);
            } else
            {
                inputString += action;
            }
            DialogBody.text = inputString;
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
        DialogObject[] dialogObjects = { new DialogObject("Hero", "Halp... I'm dying....", null, new int[] {1 }), //0
                                         new DialogObject("You", null, new string[] {"Use mushroom healing powers to save a life", "Ignore" }, new int[] {2, -1}),
                                         new DialogObject("Hero", "Thank you, I feel much better now.", null, new int[] {3}),
                                         new DialogObject("Hero", "What's your name? What are you doing here?", null, new int[] {4}),
                                         new DialogObject("Enter  Name", "InputField", null, new int[] {5}),
                                         new DialogObject("PlayerName", "My name is [name]. I'm not sure how I ended up here..", null, new int[] {6}), //5
                                         new DialogObject("Hero", "Okay, [name], where are you from?", null, new int[] {7}),
                                         new DialogObject("Enter  Home", "InputField", null, new int[] {8}),
                                         new DialogObject("Hero", "That's where the evil neko-king lives.", null, new int[] {9}),
                                         new DialogObject("Hero", "You see, I've been freeing kittens from evil brainwashing by the neko-king.", null, new int[] {10}),
                                         new DialogObject("Hero", "I've come this far, but the kitten who was helping me accidentally got brainwashed by another kitten and she attacked me.", null, new int[] {11}),
                                         new DialogObject("Hero", "They're over there right now.", null, new int[] {12}),
                                         new DialogObject("Hero", "Would you like to come with me to [home]? There's lots of kittens to save, and I could use a mushroom healer.", null, new int[] {13}),
                                         new DialogObject("PlayerName", null, new string[] {"Sure", "No Thanks"}, new int[] {14, 15}),
                                         new DialogObject("Hero", "Sounds great. Take a hold of the balloon there.", null, new int[] {16}), //14
                                         new DialogObject("Hero", "All right, have a good life!", null, new int[] {-2}), //15 leads to neutral ending
                                         new DialogObject("Hero", "Nice, let's get going.", null, new int[] {17}),
                                         new DialogObject("PlayerName", null, new string[] {"Ok", "Hold on"}, new int[] {18, 16}), //17, where scroll is halted

                                         //Level 1
                                         new DialogObject("Hero", "Beautiful morning.", null, new int[] {-5})


                                       };
                                                    
 
    /**

 LEVEL 1: MORNING CLOUDS < 9AM - 11AM - these are theoretical “times” in the day>
    -this is where the gameplay starts.
   - brainwashed cats are on clouds and are shooting fireballs with red eyes
   - hero shoots out hearts and cats return to normal and falls off cloud, following player in a line

< not quite, but close>

 Challenge will be stairs and obstacles and moving background

At the end, there will be a door which takes the player to
→ 
LEVEL 2: TILED PRISON (SODA HALL) < 11AM - 5PM >

   TODO

At the end, there will be a door which takes the player to
→ 
LEVEL 3: SETTING SUN (JAPAN) < 5PM - 7PM >

TODO

At the end, there will be a door which takes the player to
→ 
LEVEL 4: SNOWY DARKNESS (MINNESOTA) < 7PM - 9PM >

TODO

At the end, there will be a door which takes the player to
→ 
LEVEL 5: HOME < 9PM - 2AM >
   Neko King:
        Hiss!!!You will never defeat me!
       

       < lots of stairs, neko king spawns mini cats that fly>
        < hardest level >

         Hero: 
I think that was it.The cats of the world are free!

Cats: Thank you great hero!!

Hero: Thank my friend here, [Name].

Well...

Actually, I haven't known you for just today. I've known you for a long time.
[ if player's name isn't Chris, Stipes, Christopher]
In fact, I know your name isn’t[name], it's Chris.
And my name is Hina. < takes off mask. Dialog name changes to Hina >

 You've kept me safe and alive through our journey today, and I hope we can be together for the rest of our lives.
Will you marry me?

Hero:
→ OK
→ Not now

→ Ok
Hero: Good choice!
Let's get sustainably sourced engagement rings to mark our promise tomorrow.

The hero and the mushroom healer opened a mushroom soup restaurant x cat cafe together.
The happy pair lived in good health and cheer for many a long and prosperous days.
< Happy End >

→ Not now
Hero: That's cool. Thank you for helping me today. 
< Neutral End >
    **/
        return dialogObjects;
    }
}

public class DialogObject
{
    public readonly string speaker;
    public readonly string dialog;
    public string[] options;
    public readonly int[] next; //-1 = bad ending, -2 = neutral end, -3 = good end, -4=halt dialog 0 indexed dialog

    public DialogObject(string speaker, string dialog, string[] options, int[] next)
    {
        this.speaker = speaker;
        this.dialog = dialog;
        this.options = options;
        this.next = next;
    }


}
