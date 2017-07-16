using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{
    public int speed = 6;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;
        if (collider.tag == "Enemy")
        {
            if (collider.GetComponent<EnemyScript>() != null)
            {
                collider.GetComponent<EnemyScript>().Exorcised();
            }
        }
    }
}

