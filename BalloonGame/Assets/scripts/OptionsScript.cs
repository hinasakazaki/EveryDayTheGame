using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OptionsScript : MonoBehaviour {
    public Text Jump, Left, Right, Up, Down, Interact, Continue;

    public enum Actions
    {
        JUMP,
        LEFT,
        RIGHT,
        UP,
        DOWN,
        INTERACT,
        CONTINUE
    }
    private Dictionary<Actions, KeyCode> keyBindings = new Dictionary<Actions, KeyCode>();
     
	// Use this for initialization
	void Start () {
        keyBindings.Add(Actions.JUMP, KeyCode.Space);
        keyBindings.Add(Actions.LEFT, KeyCode.LeftArrow);
        keyBindings.Add(Actions.RIGHT, KeyCode.RightArrow);
        keyBindings.Add(Actions.UP, KeyCode.UpArrow);
        keyBindings.Add(Actions.DOWN, KeyCode.DownArrow);
        keyBindings.Add(Actions.INTERACT, KeyCode.X);
        keyBindings.Add(Actions.CONTINUE, KeyCode.KeypadEnter);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
