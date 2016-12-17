using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {

    public GameObject bgAudio;
    AudioSource audioSrc;
    float[] samples = new float[256];
	// Use this for initialization
	void Start () {
        audioSrc = bgAudio.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        audioSrc.GetOutputData(samples, 0);
        float max = 0.0f;
        float sum = 0.0f;
        for (int i = 0; i < 256; i++)
        {
            sum += samples[i] * samples[i];
        }
        float rms = Mathf.Sqrt(sum / 1024);
        //GetComponent<GUIText>().text = rms.ToString();
       // Debug.Log(rms.ToString());
    }
}
