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
        } else if (currString.Contains("Enter"))
        {
            KeyCode value;
            keybindings.TryGetValue(OptionsScript.Actions.CONTINUE, out value);
            this.GetComponent<Text>().text = currString.Replace("Enter", value.ToString());
        }
       
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
