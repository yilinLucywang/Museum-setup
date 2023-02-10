using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardShortcuts : MonoBehaviour
{
    public PhysicalButtonManager pbm;
    public Triggers triggers;
    public bool testState = false;
    public bool testRFID = false;
    public bool testKnives = false;
    public bool testToggles = false;
    public bool testClips = false;
    public bool testMagnets = false;
    public bool testTouch = false;
    public bool testCircuits = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (testState)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) { triggers.Trigger("Got-playMovie"); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { triggers.Trigger("Got-donePlayVideo"); }
        }
        if (testRFID)
        {
            if (Input.GetKeyDown(KeyCode.Q)) { pbm.ReceivedOSCmessage("/phidget RFID BaboonHapi"); }
            if (Input.GetKeyDown(KeyCode.W)) { pbm.ReceivedOSCmessage("/phidget RFID JackalDuamutef"); }
            if (Input.GetKeyDown(KeyCode.E)) { pbm.ReceivedOSCmessage("/phidget RFID FalconQebehsenuef"); }
            if (Input.GetKeyDown(KeyCode.R)) { pbm.ReceivedOSCmessage("/phidget RFID HumanImsety"); }
        }
        //testing toggles
        if (testToggles)
        {
            if (Input.GetKeyDown(KeyCode.Q)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 Toggle1 true"); }
            if (Input.GetKeyDown(KeyCode.W)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 Toggle2 true"); }
            if (Input.GetKeyDown(KeyCode.E)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 Toggle3 true"); }
            if (Input.GetKeyDown(KeyCode.R)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 Toggle4 true"); }
            if (Input.GetKeyDown(KeyCode.T)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 Toggle5 true"); }
            if (Input.GetKeyDown(KeyCode.Y)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 CoverToggle1 true"); }
            if (Input.GetKeyDown(KeyCode.U)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 CoverToggle2 true"); }
            if (Input.GetKeyDown(KeyCode.I)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 CoverToggle3 true"); }

            if (Input.GetKeyDown(KeyCode.A)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 Toggle1 false"); }
            if (Input.GetKeyDown(KeyCode.S)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 Toggle2 false"); }
            if (Input.GetKeyDown(KeyCode.D)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 Toggle3 false"); }
            if (Input.GetKeyDown(KeyCode.F)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 Toggle4 false"); }
            if (Input.GetKeyDown(KeyCode.G)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 Toggle5 false"); }
            if (Input.GetKeyDown(KeyCode.H)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 CoverToggle1 false"); }
            if (Input.GetKeyDown(KeyCode.J)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 CoverToggle2 false"); }
            if (Input.GetKeyDown(KeyCode.K)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 CoverToggle3 false"); }
        }
        //testing knives
        if (testKnives)
        {
            if (Input.GetKeyDown(KeyCode.Q)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 Knife1 0"); }
            if (Input.GetKeyDown(KeyCode.W)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 Knife2 0"); }
            if (Input.GetKeyDown(KeyCode.E)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 Knife3 0"); }

            if (Input.GetKeyDown(KeyCode.A)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 Knife1 999"); }
            if (Input.GetKeyDown(KeyCode.S)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 Knife2 999"); }
            if (Input.GetKeyDown(KeyCode.D)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 Knife3 999"); }
        }
        //testing clips
        if (testClips)
        {
            if (Input.GetKeyDown(KeyCode.R)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 AlClip4 true"); }
            if (Input.GetKeyDown(KeyCode.T)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 AlClip3 true"); }
            if (Input.GetKeyDown(KeyCode.Y)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 AlClip2 true"); }
            if (Input.GetKeyDown(KeyCode.U)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 AlClip1 true"); }

            if (Input.GetKeyDown(KeyCode.F)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 AlClip4 false"); }
            if (Input.GetKeyDown(KeyCode.G)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 AlClip3 false"); }
            if (Input.GetKeyDown(KeyCode.H)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 AlClip2 false"); }
            if (Input.GetKeyDown(KeyCode.J)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 AlClip1 false"); }
        }
        //testing magnets
        if (testMagnets)
        {
            if (Input.GetKeyDown(KeyCode.Q)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 MagnetS true"); }
            if (Input.GetKeyDown(KeyCode.W)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 MagnetE true"); }
            if (Input.GetKeyDown(KeyCode.E)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 MagnetH true"); }
            if (Input.GetKeyDown(KeyCode.R)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 MagnetMES true"); }

            if (Input.GetKeyDown(KeyCode.A)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 MagnetS false"); }
            if (Input.GetKeyDown(KeyCode.S)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 MagnetE false"); }
            if (Input.GetKeyDown(KeyCode.D)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 MagnetH false"); }
            if (Input.GetKeyDown(KeyCode.F)) { pbm.ReceivedOSCmessage("/phidget interfaceKit input 1 MagnetMES false"); }
        }
        //testing touch
        if (testTouch)
        {
            if (Input.GetKeyDown(KeyCode.T)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 TouchUpGods 999"); }
            if (Input.GetKeyDown(KeyCode.Y)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 TouchBowl 999"); }
            if (Input.GetKeyDown(KeyCode.U)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 TouchUpAnkhs 999"); }
            if (Input.GetKeyDown(KeyCode.I)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 TouchLowGods 999"); }
            if (Input.GetKeyDown(KeyCode.O)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 TouchLips 999"); }

            if (Input.GetKeyDown(KeyCode.G)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 TouchUpGods 0"); }
            if (Input.GetKeyDown(KeyCode.H)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 TouchBowl 0"); }
            if (Input.GetKeyDown(KeyCode.J)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 TouchUpAnkhs 0"); }
            if (Input.GetKeyDown(KeyCode.K)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 TouchLowGods 0"); }
            if (Input.GetKeyDown(KeyCode.L)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 TouchLips 0"); }
        }
        //testing Circuits
        if (testCircuits)
        {
            if (Input.GetKeyDown(KeyCode.Q)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 CircuitKneelingGods 999"); }
            if (Input.GetKeyDown(KeyCode.W)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 CircuitAnkhs 999"); }
            if (Input.GetKeyDown(KeyCode.E)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 CircuitWorshipMen 999"); }
            if (Input.GetKeyDown(KeyCode.R)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 CircuitSarcEyes 999"); }

            if (Input.GetKeyDown(KeyCode.A)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 CircuitKneelingGods 0"); }
            if (Input.GetKeyDown(KeyCode.S)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 CircuitAnkhs 0"); }
            if (Input.GetKeyDown(KeyCode.D)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 CircuitWorshipMen 0"); }
            if (Input.GetKeyDown(KeyCode.F)) { pbm.ReceivedOSCmessage("/phidget interfaceKit sensor 1 CircuitSarcEyes 0"); }
        }

    }
}
