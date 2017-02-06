using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public int index;
    public GameObject healer;

    bool radar;
    private LineRenderer line;
    private Vector3 eyePosition;
    private RaycastHit2D hit;
    GameObject hitObject;
    bool colliding = false;

    private float yValue;

    // Use this for initialization
    void Start () {
        radar = true;
        eyePosition = new Vector3(transform.position.x-0.12f, transform.position.y + 0.01f, transform.position.z);
        yValue = transform.position.y;
	}

    // Update is called once per frame
    void Update() {
        if (radar)
        {
            line = GetComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = Color.green;
            line.endColor = Color.yellow;
            line.widthMultiplier = 0.1f;
            line.sortingOrder = 1;

            Vector3[] positions = new Vector3[2];

            positions[0] = eyePosition;
            positions[1] = new Vector3(transform.position.x - Mathf.Repeat(Time.time*2, 10), transform.position.y + Mathf.Sin(Time.time * 5), transform.position.y);
            line.numPositions = positions.Length;
            line.SetPositions(positions);
            AnimationCurve curve = new AnimationCurve();

            hit = Physics2D.Raycast(positions[0], positions[1] - positions[0]);

            if (hit.collider != null)
            {
                hitObject = hit.collider.gameObject;
                if (!colliding && hitObject.GetComponent<HeroScript>() != null && hitObject.tag == "Hero")
                {
                    Debug.Log("Hit");
                    hitObject.GetComponent<HeroScript>().TakeDamage(10);
                    colliding = true;
                }
            }
            else
            {
                Debug.Log("No Hit");

                colliding = false;
            }
        } 
	}

    public void Exorcised()
    {
         radar = false;
        line.enabled = false;
        this.transform.parent = healer.transform;
        this.transform.localScale = new Vector3(1f, 1f, 0f);
        this.transform.localPosition = new Vector3(0.9f + index, -0.2f, 0f);

        if (this.GetComponentInParent<GameScript>() != null)
        {
            this.GetComponentInParent<GameScript>().OnCatExorcised(index);
        }
    }

   
}
