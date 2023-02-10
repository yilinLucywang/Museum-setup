using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalButtonManager : MonoBehaviour {

    public Triggers triggers;

    private string[] data;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void ReceivedOSCmessage(string d) {
        Debug.Log("Received Phidget OSC : " + data);
        data = d.Split(' ');
        HandleMessage();
    }

    void HandleMessage() {
        if (data.Length > 1) {
            // process /phidget messages
            if (data[0] == "/phidget") {
                // data has been received from a phidget OSC message
                // /phidget IR code
                // /phidget RFID tag
                // /phidget interfaceKit [input,sensor] INTERFACE# tag value
                // /phidget circularTouch input tag bool	[tag => cirTouchTouch, cirTouchNear] 
                // /phidget circularTouch sensor value
                // /phidget interfaceKit sensor Hole1-2 value 
                // /phidget interfaceKit input Toggle1-5 bool
                // /phidget interfaceKit input CoverToggle1-3 bool
                // /phidget interfaceKit sensor Knife1-3 value
                // /phidget interfaceKit input Key bool
                // /phidget interfaceKit input AlClip1-4 bool

                string code = "";
                // what is the function
                switch (data[1]) {
                    case "interfaceKit":
                        code = data[4];
                        if (code.Contains("Button")) {
                            bool value = bool.Parse(data[5]);
                            if (value)
                                triggers.Trigger("Got-" + code);
                            else
                                triggers.Trigger("Lost-" + code);
                        }
                        if (code.Contains("Magnet")) {
                            bool value = bool.Parse(data[5]);
                            if (value)
                                triggers.Trigger("Got-" + code);
                            else
                                triggers.Trigger("Lost-" + code);
                        }
                        if (code.Contains("Touch")) {
                            int value = int.Parse(data[5]);
                            if (value < 950)
                                triggers.Trigger("Got-" + code);
                            else
                                triggers.Trigger("Lost-" + code);
                        }
                        if (code.Contains("Circuit")) {
                            int value = int.Parse(data[5]);
                            if (value > 200) triggers.Trigger("Got-" + code);
                            else triggers.Trigger("Lost-" + code);
                        }
                        if (code.Contains("Toggle")) {      // Toggle and CoverToggle
                            bool value = bool.Parse(data[5]);
                            if (value)
                                triggers.Trigger("Got-" + code);
                            else
                                triggers.Trigger("Lost-" + code);
                        }
                        if (code.Contains("Key")) {
                            bool value = bool.Parse(data[5]);
                            if (value)
                                triggers.Trigger("Got-" + code);
                            else
                                triggers.Trigger("Lost-" + code);
                        }
                        if (code.Contains("AlClip")) {
                            bool value = bool.Parse(data[5]);
                            if (value)
                                triggers.Trigger("Got-" + code);
                            else
                                triggers.Trigger("Lost-" + code);
                        }
                        if (code.Contains("Knife")) {
                            int value = int.Parse(data[5]);
                            if (value > 10)
                                triggers.Trigger("Got-" + code);
                            else
                                triggers.Trigger("Lost-" + code);
                        }

                        break;

                    case "IR":
                        code = data[2];
                        // DO SOMETHING
                        break;
                    case "RFID":
                        code = data[2];
                        triggers.Trigger("Got-" + code);
                        break;

                    case "circularTouch":
                        if (data[2] == "input") {
                            code = data[3];
                            bool value = bool.Parse(data[4]);
                            if (value)
                                triggers.Trigger("Got-" + code);
                            else
                                triggers.Trigger("Lost-" + code);
                        } else if (data[2] == "sensor") {
                            int value = Mathf.Min((int.Parse(data[3]) / 100), 9);
                            triggers.Trigger("Got-circularTouch" + value.ToString());
                        }
                        break;
                }
            }
        }
    }
}
