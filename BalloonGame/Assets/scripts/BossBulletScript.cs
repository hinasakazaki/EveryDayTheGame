using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : CatBulletScript {

    float time = 0;
    float bossspeed;
    float r;

	// Use this for initialization
	void Start () {
        bossspeed = 10f;
        r = 0.0001f;
        Destroy(gameObject, 20.0f);
    }

    // Update is called once per frame
    public override void Update () {

        if (GameScript.nekolordExorcised)
        {
            Destroy(this);
        }

        time += Time.deltaTime*speed;

        float x = Mathf.Cos(time)*r;
        float y = Mathf.Sin(time)*r;

        transform.position = new Vector3(transform.position.x+x, transform.position.y+y, transform.position.z + 0f);

        r += 0.0001f;
    }
}
