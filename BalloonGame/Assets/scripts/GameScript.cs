using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    public Slider HealthSlider;

    private static int health;

	// Use this for initialization
	void Start () {
        health = 100;
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
}
