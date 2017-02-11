using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class InputScript : MonoBehaviour {
    //Audio
    public GameObject Audio;

    //Menu
    public GameObject hudMainMenu;
    public GameObject MainMenu;
    public Text menuOptions;

    private string[] menuOptionStrings = { "Start", " \nHow To Play", " \nOption", " \nCredits" };
    private int counter; //0 for start , 1 for how to play, 2 for option 3 for credits

    //Game
    public GameObject gameMain;
    public GameObject gameUI;
    public GameObject DialogUI;

    private bool gameStarted = false;

    // Use this for initialization
    void Start() {
        counter = 0;
        putInSelector();
    }

    // Update is called once per frame
    void Update() {

        if (!gameStarted) //this is for menu option
        {

            if (Input.GetKeyDown(KeyCode.UpArrow) && counter > 0) //up and down for NPC character
            {
                counter -= 1;
                putInSelector();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && counter < 3)
            {
                counter += 1;
                putInSelector();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                switch (counter)
                {
                    case 0:
                        startGame();
                        break;
                    case 1:
                        //How To Play Menu
                        break;

                    case 2:
                        // Options (Do we need this?
                        break;

                    case 3:
                        //Credits screen
                        break;
                }
            }
        }

        //this is for dialog time
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("Enter");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("Up");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("Down");
        }
        else if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("Delete");
        }
        else if (Input.GetKeyDown(KeyCode.A)) //I know I know you're judging me but at this point I want to write this stuff ok? please understand
        {
            DialogUI.GetComponent<DialogScript>().OnInput("a");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("b");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("c");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("d");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("e");
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("f");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("g");
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("h");
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("i");
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("j");
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("k");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("l");
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("m");
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("n");
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("o");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("p");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("q");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("r");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("s");
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("t");
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("u");
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("v");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("w");
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("x");
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("y");
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            DialogUI.GetComponent<DialogScript>().OnInput("z");
        }
    }

    private void putInSelector()
    {
        StringBuilder menuOptionString = new StringBuilder();
      
        for (int i = 0; i < menuOptionStrings.Length; i ++)
        {
            if (i == counter)
            {
                menuOptionString.Append(menuOptionStrings[i] + " <");
            }
            else
            {
                menuOptionString.Append(menuOptionStrings[i]);
            }
        }
        menuOptions.text = (menuOptionString.ToString());
    }

    private void startGame()
    {
        hudMainMenu.SetActive(false);
        MainMenu.SetActive(false);

        gameMain.SetActive(true);
        gameUI.SetActive(true);

        Audio.GetComponent<AudioManager>().changeBG(AudioManager.BGList.RISKS);
        gameStarted = true;
    }
}
