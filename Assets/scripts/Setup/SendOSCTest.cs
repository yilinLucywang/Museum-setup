using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendOSCTest : MonoBehaviour {

	public UnityEngine.UI.InputField input;

	private OSCController osc;
	private string message = "";

	// Use this for initialization
	void Start () {
		osc = GameObject.Find ("OSCMain").GetComponent<OSCController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SendTest() {
		message = input.text;
		print ("Sending Out Test: " + message);
		osc.SendOSCMessage (message);
	}
}
