using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour {
    public GameObject characterSprite;

    private LineRenderer line;

    public float posChar;
    public float posBal;

    void Start () {
        line = GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.black;
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

        posChar = characterSprite.transform.position.x;
        posBal = this.gameObject.transform.position.x;

        Vector3[] positions = new Vector3[2];

        positions[0] = characterSprite.transform.position;
        positions[1] = this.gameObject.transform.position;
        line.numPositions = positions.Length;
        line.SetPositions(positions);
        AnimationCurve curve = new AnimationCurve();

        curve.AddKey(0.01f, 0.2f);
        curve.AddKey(0.2f, 0.01f);

        line.widthCurve = curve;
        
        line.widthMultiplier = 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(positions[0], positions[1] - positions[0]);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Enemy");
            } else
            {
                Debug.Log("something else");
            }

        }
    }
}
