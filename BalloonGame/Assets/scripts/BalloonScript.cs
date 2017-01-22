using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonScript : MonoBehaviour {
    public GameObject post;
    public GameScript gameScript;
    public GameObject hero;
    public Text tutorialText;

    private LineRenderer line;
    private RaycastHit2D hit;
    private GameObject lastHit;
    GameObject hitObject;

    private GameObject currentlyAttachedTo;
    void Start () {
        lastHit = null;

        line = GetComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.gray;
        line.endColor = Color.white;

        // Set some positions
        currentlyAttachedTo = post;

        Vector3[] positions = new Vector3[2];
        positions[0] = currentlyAttachedTo.transform.position;
        positions[1] = this.gameObject.transform.position;
        line.numPositions = positions.Length;
        line.SetPositions(positions);
    }

    void Update()
    {
        Vector3[] positions = new Vector3[2];

        positions[0] = currentlyAttachedTo.transform.position;
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
                    gameScript.TakeHealOrDamage(-10);
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
         //   Debug.Log("stopped colliding");
        }
    }
    
    public void StartGrabTutorial()
    {
        tutorialText.gameObject.SetActive(true);
    }

    public void ChangeAttached(GameObject attached)
    {
        this.currentlyAttachedTo = attached;
        Animator heroAnim = hero.GetComponent<Animator>();

        //move hero
       Vector3 temp = new Vector3(0.67f, 1.65f, 0f);
        hero.transform.position += temp;
        hero.transform.Rotate(Vector3.right);
    }

    public void heroHealed()
    {
        Animator heroAnim = hero.GetComponent<Animator>();
        if (!heroAnim.GetBool("revives"))
        {
            heroAnim.SetBool("revives", true);
        }
    }
}
