using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : CatBulletScript {

    float time = 0;
    float r;
    
    // Use this for initialization
    void Start () {
        speed = 7f;
        r = 0.0001f;
        Destroy(gameObject, 20.0f);
        axis = Vector3.up;
        frequency = 5f;
        magnitude = .12f;

    }

    // Update is called once per frame
    public override void Update () {

        
        if (GameScript.nekolordExorcised)
        {
            Destroy(this);
        }
        transform.position += Vector3.left * speed * Time.deltaTime;
        transform.position += axis * Mathf.Sin(Time.time * frequency) * magnitude;
        /**
        time += Time.deltaTime*speed;

        float x = Mathf.Cos(time)*r;
        float y = Mathf.Sin(time)*r;

        transform.position = new Vector3(transform.position.x-x, transform.position.y-y, transform.position.z + 0f);

        r += 0.0001f;
    **/
    }
}
