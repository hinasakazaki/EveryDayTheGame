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
    private Animator heroAnim;

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

        heroAnim = hero.GetComponent<Animator>();
    }

    void Update()
    {
        Vector3[] positions = new Vector3[2];

        positions[0] = new Vector3(currentlyAttachedTo.transform.position.x + 0.2f , currentlyAttachedTo.transform.position.y);
        positions[1] = this.gameObject.transform.position; //when I move it left, it looks awk
        line.numPositions = positions.Length;
        line.SetPositions(positions);
        AnimationCurve curve = new AnimationCurve();

        curve.AddKey(0.01f, 0.2f);
        curve.AddKey(0.2f, 0.01f);
        curve.AddKey(0.31f, 0.3f);

        line.widthCurve = curve;
        
        line.widthMultiplier = 0.1f;

        /*
        hit = Physics2D.Raycast(positions[0], positions[1] - positions[0]);

        if (hit.collider != null)
        {
            hitObject = hit.collider.gameObject;
            if (hitObject != lastHit)
            {
                if (hitObject.tag == "Enemy")
                {
                    gameScript.TakeHealOrDamage(-10);
                }
                else
                {

                }

                lastHit = hitObject;
            }
        } else
        {

        }
        */
    }
    
    public void StartGrabTutorial()
    {
        tutorialText.gameObject.SetActive(true);
    }


    public void TriggerJumpDown()
    {
        hero.GetComponent<HeroScript>().EndShooting();

        this.GetComponent<RectTransform>().anchoredPosition = new Vector3(-7.81f, 5.47f, 0);
        //transform.localPosition = Vector3.zero;
        heroAnim.SetBool("jump", false);
        heroAnim.SetBool("unmasked", false);
        heroAnim.SetBool("comesDown", true);
    }

    public void TriggerUnmask()
    {
        heroAnim.SetBool("unmasked", true);
    }


    public void ChangeAttached(GameObject attached)
    {
        tutorialText.gameObject.SetActive(false);
        this.currentlyAttachedTo = attached;

        //move hero
        if (!heroAnim.GetBool("jump"))
        {
            heroAnim.SetBool("jump", true);
            hero.GetComponent<HeroScript>().StartShooting();
        }
    }

    public void heroHealed()
    {
        if (!heroAnim.GetBool("revives"))
        {
            heroAnim.SetBool("revives", true);
        }
    }
}
