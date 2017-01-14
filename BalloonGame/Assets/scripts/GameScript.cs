using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    public Slider HealthSlider;
    public GameObject DialogObject;
    public GameObject Audio;

    private static int health;

	// Use this for initialization
	void Start () {
        health = 0;
        DialogObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        HealthSlider.value = health;

        if (health <= 0)
        {
            //game end
        }


	}

    public static void TakeHealOrDamage(int x)
    {
        health += x;
    }

    public void LoadNewLevel(int level)
    {

    }
}
