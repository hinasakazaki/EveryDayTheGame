using UnityEngine;
using System.Collections;

public class SideScrollingScript : MonoBehaviour {

	private Vector3 startPosition;
	public float scrollSpeed;
    public float tileSizeX;

    private bool paused = true;

	// Use this for initialization
	void Start () {
		startPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!paused)
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
            transform.position = startPosition + Vector3.left * newPosition;
        }
		
	}

    public void StartScroll()
    {
        paused = false;
    }

    public void StopScroll()
    {
        paused = true;
    }
}
