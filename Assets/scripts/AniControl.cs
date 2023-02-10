using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniControl : MonoBehaviour {

    public GameObject feedbackObj;
    public bool useFeedback = false;

    private OSCController osc;
	private string message = "";

	// Use this for initialization
	void Start () {
		osc = GameObject.Find("OSCMain").GetComponent<OSCController>();
        if (!useFeedback) feedbackObj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FeedbackCube(Color color)
	{
		// the feedback cube just shows that the function was called
		feedbackObj.GetComponent<Renderer>().material.SetColor("_Color", color);
	}

	void WaitToCueLight() {
		// called by other methods, after some delay, for when a second cue is desired 
		osc.SendOSCMessage (message);
	}

	// ***********************************************************************************
	// public methods that will be called by the AudioEvents script
	// these methods send a string message to the lighting controller through OSC networking
	// to format the the message see the "OSC string messages to the lighting controller" in "CAVE USE.txt"  

	public void Blackout() {
		osc.SendOSCMessage ("/lighting operations blackout");
		FeedbackCube(Color.black);
	}

	public void AllOn() {
		osc.SendOSCMessage ("/lighting operations allOn");
		FeedbackCube(Color.white);
    }

	public void SetYellow() {
		osc.SendOSCMessage ("/lighting color themeRGB 255 128 0 0 255");
		FeedbackCube(Color.yellow);
    }

	public void SetRed() {
		osc.SendOSCMessage ("/lighting color themeRGB 255 0 0 0 255");
		FeedbackCube(Color.red);
    }

	public void SetBlue() {
		osc.SendOSCMessage ("/lighting colorAdd themeRGB 100 100 255 0 255");
		FeedbackCube(Color.blue);
    }

	public void SetPurple() {
		osc.SendOSCMessage ("/lighting colorAdd themeRGB 255 0 255 0 255");
		FeedbackCube(Color.magenta);
    }

	public void Platform() {
		osc.SendOSCMessage ("/lighting colorAdd walls 255 0 0 0 255");
		message = "/lighting colorAdd Guest 0 0 255 0 255";
		Invoke("WaitToCueLight",0.05f);
		FeedbackCube(Color.red);
    }

	public void WallsGreen() {
		osc.SendOSCMessage ("/lighting fadeAdd walls 0 255 0 0 255");
		FeedbackCube(Color.green);
    }

	public void GuestBlue() {
		osc.SendOSCMessage ("/lighting color Guest 0 0 255 0 255");
		FeedbackCube(Color.blue);
    }

	public void CueFadeOut() {
		osc.SendOSCMessage ("/lighting cue Tour 1");
		FeedbackCube(Color.black);
    }


}
