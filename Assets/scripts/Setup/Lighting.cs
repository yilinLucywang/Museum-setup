using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour {

	private OSCController osc;
	private string message = "";
	private bool inUse = false;

	// Use this for initialization
	void Start () {
		osc = GameObject.Find ("OSCMain").GetComponent<OSCController> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Blackout() {
		osc.SendOSCMessage ("/lighting operations blackout");
	}

	public void SetupLighting() {
		if (!inUse) {		// prevent double clicking the button
			StartCoroutine ("SendLighting");
			inUse = true;
		}
	}

	IEnumerator SendLighting() {
		osc.SendOSCMessage ("/lighting fade center 255 255 255 255 255");
		yield return new WaitForSeconds (2.5f);
		osc.SendOSCMessage ("/lighting fadeAdd SL1 0 0 255 0 255");
		yield return new WaitForSeconds (2f);
		osc.SendOSCMessage ("/lighting fadeAdd SR1 255 128 0 0 255");
		yield return new WaitForSeconds (2.5f);
		osc.SendOSCMessage ("/lighting fadeAdd SL2 255 0 255 0 255");
		yield return new WaitForSeconds (2f);
		osc.SendOSCMessage ("/lighting colorAdd SR2 255 0 0 0 255");
		print ("done");
		yield return new WaitForSeconds (2.5f);
		inUse = false;
	}
}
