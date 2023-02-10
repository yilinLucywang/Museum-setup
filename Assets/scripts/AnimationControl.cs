using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

	public GameObject[] objects;

	private OSCController osc;
	private string message = "";

	// Use this for initialization
	void Start () {
		osc = GameObject.Find("OSCMain").GetComponent<OSCController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CueLight(string data) {
		// called from AudioEventsData => cue a light function
		switch (data) { 
		case "blackout":
			osc.SendOSCMessage ("/lighting operations blackout");
			break;
		case "allOn":
			osc.SendOSCMessage ("/lighting operations allOn");
			break;
		case "setYellow":
			osc.SendOSCMessage ("/lighting color themeRGB 255 128 0 0 255");
			break;
		case "setRed":
			osc.SendOSCMessage ("/lighting color themeRGB 255 0 0 0 255");
			break;
		case "setBlue":
			osc.SendOSCMessage ("/lighting colorAdd themeRGB 100 100 255 0 255");
			break;
		case "setPurple":
			osc.SendOSCMessage ("/lighting colorAdd themeRGB 255 0 255 0 255");
			break;
		case "curtainGreen":
			osc.SendOSCMessage ("/lighting operations curtain green");
			osc.SendOSCMessage ("/lighting operations curtain on");
			osc.SendOSCMessage ("/lighting operations curtain green");
			break;
		case "curtainOff":
			osc.SendOSCMessage ("/lighting operations curtain off");
			break;
		case "wallsGreen":
			osc.SendOSCMessage ("/lighting fadeAdd walls 0 255 0 0 255");
			break;
		case "platform":
			osc.SendOSCMessage ("/lighting colorAdd walls 255 0 0 0 255");
			message = "/lighting colorAdd Guest 0 0 255 0 255";
			Invoke("WaitToCueLight",0.05f);
			break;
		case "guestBlue":
			osc.SendOSCMessage ("/lighting color Guest 0 0 255 0 255");
			break;
		case "cueFadeout":
			osc.SendOSCMessage ("/lighting cue Tour 1");
			break;
		case "":
			Debug.Log ("Event Trigger with no Data");
			break;
		default:
			Debug.Log ("Event Trigger with Data not found: " + data);
			break;
		}
	}

	void WaitToCueLight(){
		// send the last saved message out
		osc.SendOSCMessage (message);
	}

	public void CueObject(string data) {
		// called from AudioEventsData => cue an object function
		switch (data) { 
		case "ball":
			GameObject.Instantiate(objects[0], new Vector3(Random.value, Random.value, Random.value), Quaternion.identity);
			break;
		case "":
			Debug.Log ("Event Trigger with no Data");
			break;
		default:
			Debug.Log ("Event Trigger with Data not found: " + data);
			break;
		}
	}


}
