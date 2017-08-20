using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour {
    public GameObject heartBullet;
    public GameObject spawnpoint;
    private int freq, counter = 0;
    private bool shooting;
	// Use this for initialization
	void Start () {
        freq = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (shooting)
        {
            if (counter > freq)
            {
                counter = 0;
            }
            else if (counter == freq)
            {
                GameObject instantiated = Instantiate(heartBullet, new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y, spawnpoint.transform.position.z), Quaternion.identity);
                instantiated.GetComponent<Animator>().SetFloat("health", freq);
            }
            counter += 1;
        }
        
    }

    public void StartShooting()
    {
        shooting = true;
    }

    public void EndShooting()
    {
        shooting = false;
    }

    public void UpdateFrequency(int health)
    {
        freq = health;
    }

    public void TakeDamage(int x)
    {
        this.GetComponentInParent<MoveScript>().TakeDamage(x);
    }
}
