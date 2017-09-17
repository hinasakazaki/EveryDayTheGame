using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour {

    public void OnKeybindingChanged(object sender, Dictionary<OptionsScript.Actions, KeyCode> keybindings)
    {
        string currString = this.GetComponent<Text>().text;

        if (currString.Contains("X"))
        {
            KeyCode value;
            keybindings.TryGetValue(OptionsScript.Actions.INTERACT, out value);
            this.GetComponent<Text>().text = currString.Replace("X", value.ToString());
        }
        if (currString.Contains("Enter"))
        {
            KeyCode value;
            keybindings.TryGetValue(OptionsScript.Actions.CONTINUE, out value);
            this.GetComponent<Text>().text = currString.Replace("Enter", value.ToString());
        }
        if (currString.Contains("Left Arrow"))
        {
            KeyCode value;
            keybindings.TryGetValue(OptionsScript.Actions.LEFT, out value);
            this.GetComponent<Text>().text = currString.Replace("Left Arrow", value.ToString());
        }
        if (currString.Contains("Right Arrow"))
        {
            KeyCode value;
            keybindings.TryGetValue(OptionsScript.Actions.RIGHT, out value);
            this.GetComponent<Text>().text = currString.Replace("Right Arrow", value.ToString());
        }
        if (currString.Contains("Up Arrow"))
        {
            KeyCode value;
            keybindings.TryGetValue(OptionsScript.Actions.UP, out value);
            this.GetComponent<Text>().text = currString.Replace("Up Arrow", value.ToString());
        }
        if (currString.Contains("Down Arrow"))
        {
            KeyCode value;
            keybindings.TryGetValue(OptionsScript.Actions.DOWN, out value);
            this.GetComponent<Text>().text = currString.Replace("Down Arrow", value.ToString());

        }
        if (currString.Contains("Space"))
        {
            KeyCode value;
            keybindings.TryGetValue(OptionsScript.Actions.JUMP, out value);
            this.GetComponent<Text>().text = currString.Replace("Space", value.ToString());

        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
