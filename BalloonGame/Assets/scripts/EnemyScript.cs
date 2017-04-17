using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public int index;
    public GameObject healer;

    public bool radar;
    private LineRenderer line;
    private Vector3 eyePosition;
    private RaycastHit2D hit;
    GameObject hitObject;
    bool colliding = false;

    private float yValue;

    // Use this for initialization
    void Start () {
        yValue = transform.position.y;
        this.line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (radar)
        {
            eyePosition = new Vector3(transform.position.x - 0.12f, transform.position.y + 0.01f, transform.position.z);

            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = Color.green;
            line.endColor = Color.yellow;
            line.widthMultiplier = 0.1f;
            line.sortingOrder = 1;

            Vector3[] positions = new Vector3[2];

            positions[0] = eyePosition;
            positions[1] = new Vector3(transform.position.x - Mathf.Repeat(Time.time*2, 10), transform.position.y + Mathf.Sin(Time.time * (index+1) *  5));
            line.numPositions = positions.Length;
            line.SetPositions(positions);

            // line.SetPositions(positions);
            //   AnimationCurve curve = new AnimationCurve();

            /** For 3d, proven to be accurate 
            var heading = positions[1] - positions[0];
            var distance = heading.magnitude;
            var direction = heading / distance; // This is now the normalized direction.

            Debug.DrawRay(positions[0], direction, Color.red);

             */
            //vector2 origin vector2 direction vfloat distance
            //hit = Physics2D.Raycast(positions[0], heading, distance);
            Vector2 eyePosition2D = new Vector2(eyePosition.x, eyePosition.y);
            Vector2 endOfLinePosition2D = new Vector2(positions[1].x, positions[1].y);

           /// /* this is for 2D
            var heading = endOfLinePosition2D - eyePosition2D;
            var distance = heading.magnitude;
            var direction = heading / distance; // This is now the normalized direction.

            Debug.DrawRay(endOfLinePosition2D, direction, Color.red);
            //*/
            hit = Physics2D.Raycast(endOfLinePosition2D, direction, distance, LayerMask.NameToLayer("Enemy")); //added anti mask for self. which seems to be working
           // , 1 << LayerMask.NameToLayer("Enemy"))
            if (hit)
            {
                Debug.Log("hello");
            }
            //if (index == 0) Debug.Log("eyePos2d" + eyePosition2D + "endOfLine2d" + endOfLinePosition2D); raycast location should be accurate! 

            if (hit.collider != null)
            {

                hitObject = hit.collider.gameObject;
                Debug.Log("HIit" + hitObject.name + index);

                if (!colliding && hitObject.GetComponent<HeroScript>() != null && hitObject.tag == "Hero")
                {
                    Debug.Log("Hit with cat" + index);
                    hitObject.GetComponent<HeroScript>().TakeDamage(10);
                    colliding = true;
                }
            }
            else
            {
                colliding = false;
            }
        } 
	}

    public void Exorcised()
    {
        this.radar = false;
        this.line.enabled = false;

        this.transform.parent = healer.transform;
        this.transform.localScale = new Vector3(1f, 1f, 0f);
        this.transform.localPosition = new Vector3(0.9f + index, -0.2f, 0f);

        if (this.GetComponentInParent<GameScript>() != null)
        {
            this.GetComponentInParent<GameScript>().OnCatExorcised(index);
        }
    }

   
}
