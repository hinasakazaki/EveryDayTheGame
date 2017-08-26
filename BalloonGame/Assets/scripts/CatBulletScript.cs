using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBulletScript : MonoBehaviour {

    public float speed = 5.0f;

    public float frequency = 0.5f;  // Speed of sine movement
    public float magnitude = 0.5f;   // Size of sine movement
    private Vector3 axis;

    private Vector3 pos;
    bool colliding = false;

    // Use this for initialization
    void Start () {
        axis = Vector3.up;
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update () {
        transform.position += Vector3.left * speed * Time.deltaTime;
        transform.position += axis * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;
         if (!colliding && collider.GetComponent<HeroScript>() != null && collider.tag == "Hero")
            {
                collider.GetComponent<HeroScript>().TakeDamage(10);
                colliding = true;
            }
         else
        {
            colliding = false;
        }
    }
}
