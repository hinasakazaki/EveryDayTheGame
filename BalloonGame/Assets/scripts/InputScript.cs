using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class InputScript : MonoBehaviour {

    //Menu
    public GameObject hudMainMenu;
    public GameObject MainMenu;
    public Text menuOptions;

    private string[] menuOptionStrings = { "Start"," \nHow To Play", " \nOption", " \nCredits" };
    private int counter; //0 for start , 1 for how to play, 2 for option 3 for credits

    //Game
    public GameObject gameMain;
    public GameObject healthUI;
    public GameObject DialogUI;

    // Use this for initialization
    void Start () {
        counter = 0;
        putInSelector();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.UpArrow) && counter > 0) //up and down for NPC character
        {            counter -= 1;            putInSelector();        }        if (Input.GetKeyDown(KeyCode.DownArrow) && counter < 3 )        {
            counter += 1;            putInSelector();        }

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
        healthUI.SetActive(true);
        DialogUI.SetActive(true);
    }
}
