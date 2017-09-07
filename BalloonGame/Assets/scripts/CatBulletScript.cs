using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBulletScript : MonoBehaviour {

    public float speed = 1.0f; //maybe randomize between 1 and 3

    public float frequency = 2f;  // Speed of sine movement, maybe randomize between 2 and 1
    public float magnitude = .01f;   // Size of sine movement, maybe randomize between 0.03 and 0.01
    private Vector3 axis;

    private Vector3 pos;
    bool colliding = false;

    // Use this for initialization
    void Start () {
        axis = Vector3.up;
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    public virtual void Update () {
        transform.position += Vector3.left * speed * Time.deltaTime;
        transform.position += axis * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;
         if (!colliding && collider.GetComponent<HeroScript>() != null && collider.tag == "Hero")
            {
                collider.GetComponent<HeroScript>().TakeDamage(5);
                colliding = true;
                Destroy(this.gameObject);
        }
        else
        {
            colliding = false;
        }
    }
}
