﻿using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour {

    public static GazeGestureManager Instance { get;  private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

	// Use this for initialization
	void Awake ()
	{
	    Instance = this;

	    // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
	    recognizer.TappedEvent += (source, tapCount, ray) =>
	    {
	        // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
	        {
	            FocusedObject.SendMessageUpwards("OnSelect", SendMessageOptions.DontRequireReceiver);
	        }
	    };
        recognizer.StartCapturingGestures();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    // Figure out which hologram is focused this frame.
	    GameObject oldFocusObject = FocusedObject;

	    // Do a raycast into the world based on the user's
	    // head position and orientation.
	    var headPosition = Camera.main.transform.position;
	    var gazeDirection = Camera.main.transform.forward;

	    RaycastHit hitInfo;
	    FocusedObject = Physics.Raycast(headPosition, gazeDirection, out hitInfo) ? hitInfo.collider.gameObject : null;

	    // If the focused object changed this frame,
	    // start detecting fresh gestures again.
	    if (FocusedObject != oldFocusObject)
	    {
	        recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
	    }
    }
}