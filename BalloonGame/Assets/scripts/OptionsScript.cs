using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OptionsScript : MonoBehaviour {
    public Text[] actions; //size 7
    public GameObject mainMenu;
    //Jump, Left, Right, Up, Down, Interact, Continue;
    private Actions currentSelected = Actions.JUMP;
    private bool setCurrent = false;
    private string currString;

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

    public Dictionary<Actions, KeyCode> GetKeyBindings()
    {
        return keyBindings;
    }
	
	// Update is called once per frame
	void Update ()
    {
        currString += Input.inputString;
        if (currString.ToLower().Contains("ok"))
        {
            Debug.Log("Done with key bind setting, returning to main menu");
            this.gameObject.SetActive(false);
            mainMenu.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.Return) && setCurrent) //we've already set current, now time to update next one
        {
            actions[(int)currentSelected].fontSize = 20;
            if ((int)currentSelected == 6)
            {
                currentSelected = 0;
            }
            else
            {
                currentSelected += 1;
            }
            actions[(int)currentSelected].fontSize = 30;
            setCurrent = false;
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            keyBindings[currentSelected] = KeyCode.W;
            actions[(int)currentSelected].text = "W";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            keyBindings[currentSelected] = KeyCode.A;
            actions[(int)currentSelected].text = "A";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            keyBindings[currentSelected] = KeyCode.S;
            actions[(int)currentSelected].text = "S";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            keyBindings[currentSelected] = KeyCode.D;
            actions[(int)currentSelected].text = "D";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            keyBindings[currentSelected] = KeyCode.LeftShift;
            actions[(int)currentSelected].text = "Left Shift";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.RightShift))
        {
            keyBindings[currentSelected] = KeyCode.RightShift;
            actions[(int)currentSelected].text = "Right Shift";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            keyBindings[currentSelected] = KeyCode.LeftControl;
            actions[(int)currentSelected].text = "Left Control";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.RightControl))
        {
            keyBindings[currentSelected] = KeyCode.RightControl;
            actions[(int)currentSelected].text = "Right Control";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            keyBindings[currentSelected] = KeyCode.KeypadEnter;
            actions[(int)currentSelected].text = "Keypad Enter";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            keyBindings[currentSelected] = KeyCode.Return;
            actions[(int)currentSelected].text = "Enter";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            keyBindings[currentSelected] = KeyCode.Delete;
            actions[(int)currentSelected].text = "Delete";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            keyBindings[currentSelected] = KeyCode.Tab;
            actions[(int)currentSelected].text = "Tab";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            keyBindings[currentSelected] = KeyCode.X;
            actions[(int)currentSelected].text = "X";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.I))
        {
            keyBindings[currentSelected] = KeyCode.I;
            actions[(int)currentSelected].text = "I";
            setCurrent = true;
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            keyBindings[currentSelected] = KeyCode.E;
            actions[(int)currentSelected].text = "E";
            setCurrent = true;
        }

        
    }

}
