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
        AT_HOME = 1,
        MOMENT_OF_JOY = 2,
        RISKS = 3,
        DEPARTING_SOULS = 4,
        DEPARTING_SOULS_TROMBONE = 5,
        PONDERING1 = 6,
        PONDERING2 = 7,
        PONDERING3 = 8,
        PONDERING_SNOW = 9
    }

    public enum SFXList
    {
        HEAL = 1,
        DAMAGE = 2, 
        CUTEMEOW1 = 3,
        CUTEMEOW2 = 4,
        CUTEMEOW3 = 5,
        CUTEMEOW4 = 6,
        CUTEMEOW5 = 7,
        CUTEMEOW6 = 8,
        HISS = 9,
        ANGRYMEOW = 10
    }

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playSFX(SFXList sfx)
    {
        SFX.PlayOneShot(sfxList[(int)sfx - 1], 0.8f);
    }

    public void changeBG(BGList song)
    {
        BG.clip = ostList[(int)song - 1];
        BG.PlayDelayed(0.2f);
        BG.loop = true;
    }


}
