using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour {
    public GameObject characterSprite;

    private LineRenderer line;
    private RaycastHit2D hit;
    private GameObject lastHit;
    GameObject hitObject;

    void Start () {
        lastHit = null;

        line = GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.gray;
        line.endColor = Color.white;

        // Set some positions
        Vector3[] positions = new Vector3[2];
        positions[0] = characterSprite.transform.position;
        positions[1] = this.gameObject.transform.position;
        line.numPositions = positions.Length;
        line.SetPositions(positions);
    }

    void Update()
    {
        Vector3[] positions = new Vector3[2];

        positions[0] = characterSprite.transform.position;
        positions[1] = this.gameObject.transform.position;
        line.numPositions = positions.Length;
        line.SetPositions(positions);
        AnimationCurve curve = new AnimationCurve();

        curve.AddKey(0.01f, 0.2f);
        curve.AddKey(0.2f, 0.01f);
        curve.AddKey(0.31f, 0.3f);

        line.widthCurve = curve;
        
        line.widthMultiplier = 0.1f;

        hit = Physics2D.Raycast(positions[0], positions[1] - positions[0]);
        if (hit.collider != null)
        {
            hitObject = hit.collider.gameObject;
            if (hitObject != lastHit)
            {
                if (hitObject.tag == "Enemy")
                {
                    GameScript.TakeHealOrDamage(-10);
                    Debug.Log("Enemy");
                }
                else
                {
                    Debug.Log("something else");
                }

                lastHit = hitObject;
            }
        } else
        {
            Debug.Log("stopped colliding");
        }
    }
    
}
