using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour {

    public Text DialogName;
    public Text DialogBody;

    DialogObject[] dialogObject;
    int counter = 0;

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
        DialogObject curDialog = dialogObject[counter];
        DialogName.text = curDialog.name;
        DialogBody.text = curDialog.dialog;
        counter += 1;
    }

    public void OnInputChanged()
    {

    }

    private DialogObject[] InitializeDialog()
    {
        DialogObject[] dialogObjects = { new DialogObject("Hero", "Halp... I'm dying....", null),
                                         new DialogObject("You", null, new string[] {"Use mushroom healing powers to save a life", "Ignore" }),
                                         new DialogObject("Hero", "Thank you, I feel much better now.", null),
                                         new DialogObject("Hero", "What's your name? What are you doing here?", null),
                                         new DialogObject("Enter Name", "InputField", null),
                                         new DialogObject("PlayerName", "My home is [home].", null),
                                         new DialogObject("Hero", "That's where the evil neko-king lives.", null),
                                         new DialogObject("Hero", "You see, I've been freeing kittens from evil brainwashing by the neko-king.", null),
                                         new DialogObject("Hero", "I've come this far, but the kitten who was helping me accidentally got brainwashed by another kitten and she attacked me.", null),
                                         new DialogObject("Hero", "They're over there right now.", null),
                                         new DialogObject("Hero", "Would you like to come with me to [home]? There's lots of kittens to save, and I could use a mushroom healer.", null),
                                         new DialogObject("PlayerName", null, new string[] {"Sure", "No Thanks"}),
                       
                                       };
                                                    
 
        return dialogObjects;
    }
}

public class DialogObject
{
    public readonly string name;
    public readonly string dialog;

    string[] options;

    public DialogObject(string name, string dialog, string[] options)
    {
        this.name = name;
        this.dialog = dialog;
        this.options = options;
    }


}
