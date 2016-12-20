using UnityEngine;
using System.Collections;

public class SideScrollingScript : MonoBehaviour {

	private Vector3 startPosition;
	public float scrollSpeed;
    public float tileSizeX;

	// Use this for initialization
	void Start () {
		startPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeX);
        transform.position = startPosition + Vector3.left * newPosition;
	}
}
