using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour {
    public GameObject heartBullet;
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
                var spawnPoint = new Vector2(transform.position.x, transform.position.y);
                Instantiate(heartBullet, spawnPoint, Quaternion.identity);
            }
            counter += 1;
        }
        
    }

    public void StartShooting()
    {
        shooting = true;
    }

    public void TakeDamage(int x)
    {
        Debug.Log("Taking damage at hero " + x);
        this.GetComponentInParent<MoveScript>().TakeDamage(x);
        freq += 10;
    }
}
