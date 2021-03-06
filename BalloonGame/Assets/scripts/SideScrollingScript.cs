﻿using UnityEngine;
using System.Collections;

public class SideScrollingScript : MonoBehaviour {

	private Vector3 startPosition;
	public float scrollSpeed;
    public float tileSizeX;
    public float limit;

    private bool paused = true;

	// Use this for initialization
	void Start () {
		startPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!paused && transform.localPosition.x > limit)
        {
            transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
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
