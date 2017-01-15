using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip[] ostList = new AudioClip[5];
    public AudioClip[] sfxList = new AudioClip[5];

    public AudioSource BG;
    public AudioSource SFX;

    public enum BGList
    {
        AT_HOME,
        TBD,
        MOMENT_OF_JOY,
        RISKS,
        DEPARTING_SOUL
    }

    public enum SFXList
    {
        HEAL = 1,
        DAMAGE, 
        MEOW1,
        MEOW2,
        MEOW3
    }

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playSFX(SFXList sfx)
    {
        SFX.clip = sfxList[(int)sfx - 1];
        SFX.Play();
    }

    public void changeBG(BGList song)
    {
        BG.clip = ostList[(int)song - 1];
        BG.Play();
    }


}
