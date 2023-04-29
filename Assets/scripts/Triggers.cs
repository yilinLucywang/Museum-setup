using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Triggers : MonoBehaviour {

    public PlayMovie video;
    public Text statusFeedback;
    public Text correctnessStatus;
    // public GameObject startScreen;
    // public GameObject endScreen;
    public AudioSource waitingMusic;
    public Text infos;

    public AudioSource acWin; 
    public AudioSource acLose;
    public AudioSource acAllLose;

    public GameObject L1N; 
    public GameObject L1R; 

    public GameObject L2N; 
    public GameObject L2R;

    public int wrongCnt = 0;

    private List<bool> ClipStatus = new List<bool>();
    private List<bool> KnifeStatus = new List<bool>();
    private List<bool> ToggleStatus = new List<bool>(); 
    private List<bool> ToggleAns = new List<bool>{true, true, true, false};
    private List<bool> CoverToggleStatus = new List<bool>();
    private List<bool> CoverToggleAns = new List<bool>{true, true};

    [System.Serializable]
    public class LightCueData
    {
        public string function;     // the function to be sent to the showControl
        public string group;        // the group name to be sent to the ShowControl
        [Range(0,255)]
        public int red;             // the red value to be sent to the ShowControl
        [Range(0, 255)]
        public int green;           // the green value to be sent to the ShowControl
        [Range(0, 255)]
        public int blue;            // the blue value to be sent to the ShowControl
        [Range(0, 255)]
        public int amber;           // the amber value to be sent to the ShowControl
        [Range(0, 255)]
        public int dimmer;          // the dimmer value to be sent to the ShowControl
    }

    [System.Serializable]
    public class LightCue
    {
        public string name;         // a discriptive cue name for organization in inspector
        public LightCueData cue;     // the function to be sent to the showControl
    }

    public LightCue[] lightcues;

    private string state;
    private string status;
    private OSCController osc;
    private int ledON = 100;
    private int ledOFF = 0;
    private Dictionary<string, LightCueData> lCues;

    void Awake(){
        L1R.SetActive(false);
        L2R.SetActive(false);
    }
    // Use this for initialization
    void Start () {
        state = "preShow";
        StartCoroutine(WaitToTrigger("Start", 1.0f)); // wait for everything to load
        osc = GameObject.Find("OSCMain").GetComponent<OSCController>();
        status = "In Start";
        osc.SendOSCMessage("/lighting operations blackout");
        // build a dictionary of lighting cues to make the code easier 
        lCues = new Dictionary<string, LightCueData>();
        for (int i = 0; i < lightcues.Length; i++)
        {
            lCues.Add(lightcues[i].name, lightcues[i].cue);
        }

        for(int i = 0; i < 4; i++){
            KnifeStatus.Add(false);
        }

        for(int i = 0; i < 3; i++){
            ClipStatus.Add(false);
        }

        for(int i = 0; i < 3; i++){
            CoverToggleStatus.Add(false);
        }

        for(int i = 0; i < 5; i++){
            ToggleStatus.Add(false);
        }
    }

    void OnDisable() {
        Debug.Log("turning all LEDs off");
        osc.SendOSCMessage("/phidget LED setAll 0");
    }

    // Update is called once per frame
    void Update () {
        statusFeedback.text = status;
    }

    IEnumerator WaitToState(string s, float delay) {
        yield return new WaitForSeconds(delay);
        state = s;
    }

    IEnumerator WaitToTrigger(string trigger, float delay) {
        yield return new WaitForSeconds(delay);
        Trigger(trigger);
    }

    string BuildLightMessage(string name)
    {
        string s = "/lighting " +
            lCues[name].function + " " +
            lCues[name].group + " " +
            lCues[name].red.ToString() + " " +
            lCues[name].green.ToString() + " " +
            lCues[name].blue.ToString() + " " +
            lCues[name].amber.ToString() + " " +
            lCues[name].dimmer.ToString();
        return (s);
    }

    public void Trigger(string trigger) {
        Debug.Log("Handel trigger - " + trigger);
        ToggleController tc = gameObject.GetComponent<ToggleController>();
        //CoverToggleController ctc = gameObject.GetComponent<CoverToggleController>();
        RotationController rc = gameObject.GetComponent<RotationController>();
        multipleTouch mt = gameObject.GetComponent<multipleTouch>();
        switch (trigger) {
            ////////////////////// STATE CONTROL ///////////////////////////////////////////////
            case "Start":
                if (state == "preShow") {
                    state = "preShow";
                    status = "preshow";
                    // lighting - orange all walls    -- waiting light
                    osc.SendOSCMessage(BuildLightMessage("preShow"));  // preshow
                    // make sure start screen is visible
                    //startScreen.SetActive(true);
                    //endScreen.SetActive(false); // hide until needed
                    // playing waiting music
                    waitingMusic.Play();
                    // set the new state and update status
                    state = "wait";
                    status = "waiting for guest";
                }
                break;
            case "Got-playMovie":
                if (state == "wait") {
                    // set the new state and update status
                    state = "play";
                    status = "playing movie";
                    // hide the start screen
                    //startScreen.SetActive(false);
                    // stop the waiting music
                    waitingMusic.Stop();
                    // start video playback
                    video.PlayVideo();
                    // lighting - orange all walls      -- starting playback light (not needed if DARKACE has a key at frame 0
                    osc.SendOSCMessage(BuildLightMessage("play"));  // play
                    // trigger done state after video playbeack
                    WaitToTrigger("Got-donePlayVideo", video.GetMovieLength());
                }
                break;
            case "Got-donePlayVideo":
                if (state == "play") {
                    // set the new state and update status
                    state = "preShow";
                    status = "waiting for guest to leave";
                    // stop video playback - in case we jumpped into this state early
                    video.StopVideo();
                    // show the endScreen to have something to look at 
                    //endScreen.SetActive(true);
                    // lighting - orange all walls      -- Guest leave lighting
                    osc.SendOSCMessage(BuildLightMessage("leave"));  // leave
                    // trigger wait so guest has time to leave
                    StartCoroutine(WaitToTrigger("Start", 3.0f)); // wait for guest to leave
                }
                break;

            /////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////// TOMB PHIDGETS ////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////

            ////////////////////////////////////// FLOOR PHIDGETS ///////////////////////////////////////////
            case "Got-floorButtonUSC":
                if (state == "wait" && status == "waiting for guest") {
                    status = "guest has stepped on the floor";
                    StartCoroutine(WaitToTrigger("Got-playMovie", 1.0f)); // start video playback
                }
                break;
            case "Got-floorButtonDSC":
                if (state == "wait" && status == "waiting for guest") {
                    status = "guest has stepped on the floor";
                    StartCoroutine(WaitToTrigger("Got-playMovie", 1.0f)); // start video playback
                }
                break;
            case "Got-floorButtonSL2":
                if (state == "wait" && status == "waiting for guest") {
                    status = "guest has stepped on the floor";
                    StartCoroutine(WaitToTrigger("Got-playMovie", 1.0f)); // start video playback
                }
                break;
            case "Got-floorButtonSR1":
                if (state == "wait" && status == "waiting for guest") {
                    status = "guest has stepped on the floor";
                    StartCoroutine(WaitToTrigger("Got-playMovie", 1.0f)); // start video playback
                }
                break;
            case "Got-floorButtonSR2":
                if (state == "wait" && status == "waiting for guest") {
                    status = "guest has stepped on the floor";
                    StartCoroutine(WaitToTrigger("Got-playMovie", 1.0f)); // start video playback
                }
                break;
            case "Got-floorButtonSL1":
                if (state == "wait" && status == "waiting for guest") {
                    status = "guest has stepped on the floor";
                    StartCoroutine(WaitToTrigger("Got-playMovie", 1.0f)); // start video playback
                }
                break;

            ////////////////////////////////////// TOUCH PHIDGETS ///////////////////////////////////////////

            case "Got-TouchBird":

                break;
            case "Got-TouchBowl":

                break;
            case "Got-TouchWater":

                break;
            case "Got-TouchArm":

                break;
            case "Got-TouchLowGods":

                break;
            case "Got-TouchUpAnkhs":

                break;
            case "Got-TouchLowAnkhs":

                break;
            case "Got-TouchUpGods":

                break;
            case "Got-TouchLips":

                break;
            case "Lost-TouchLips":

                break;

            ////////////////////////////////////// MAGNET PHIDGETS ///////////////////////////////////////////

            case "Got-MagnetE":

                break;
            case "Got-MagnetH":

                break;
            case "Got-MagnetA":

                break;
            case "Got-MagnetMES":

                break;
            case "Got-MagnetS":

                break;
            case "Got-MagnetY":

                break;

            ////////////////////////////////////// COMPLETE CIRCUIT PHIDGETS ///////////////////////////////////////////

            case "Got-CircuitAnkhs":

                break;
            case "Got-CircuitSarcEyes":

                break;
            case "Got-CircuitWorshipMen":

                break;
            case "Got-CircuitKneelingGods":

                break;

            ////////////////////////////////////////////// RFID ////////////////////////////////////////////////////////

            case "Got-BaboonHapi":

                break;
            case "Got-JackalDuamutef":

                break;
            case "Got-FalconQebehsenuef":

                break;
            case "Got-HumanImsety":

                break;


            /////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////// KIOSK PHIDGETS ///////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////
        
            case "Got-Toggle1":
                // turn on LED feedback
                LEDFeedback("Toggle1", ledON);
                ToggleStatus[0] = true;
                break;
            case "Got-Toggle2":
                // turn on LED feedback
                LEDFeedback("Toggle2", ledON);
                ToggleStatus[1] = true;
                break;
            case "Got-Toggle3":
                // turn on LED feedback
                LEDFeedback("Toggle3", ledON);
                ToggleStatus[2] = true;
                break;
            case "Got-Toggle4":
                // turn on LED feedback
                LEDFeedback("Toggle4", ledON);
                ToggleStatus[3] = true;
                break;
            case "Got-Toggle5":
                // turn on LED feedback
                LEDFeedback("Toggle5", ledON);
                ToggleStatus[4] = true;
                int cnt = 0;
                for(int i = 0; i < ToggleAns.Count; i++){
                    if(ToggleAns[i] == ToggleStatus[i]){
                        cnt += 1;
                    }
                }
                if(cnt == 4){
                    infos.text = "correct";
                    acWin.Play();
                }
                else{
                    infos.text = "incorrect";
                    wrongCnt += 1; 
                    if(wrongCnt == 1){
                        acLose.Play();
                        L1N.SetActive(false);
                        L1R.SetActive(true);
                        acLose.Play();
                    }
                    else{
                        L1N.SetActive(false); 
                        L2N.SetActive(false); 

                        L1R.SetActive(true); 
                        L2R.SetActive(true);
                        acAllLose.Play();
                    }
                }
                break;
            case "Lost-Toggle1":
                // turn off LED feedback
                LEDFeedback("Toggle1", ledOFF);
                ToggleStatus[0] = false;
                break;
            case "Lost-Toggle2":
                // turn off LED feedback
                LEDFeedback("Toggle2", ledOFF);
                ToggleStatus[1] = false;
                break;
            case "Lost-Toggle3":
                // turn off LED feedback
                LEDFeedback("Toggle3", ledOFF);
                ToggleStatus[2] = false;
                break;
            case "Lost-Toggle4":
                // turn off LED feedback
                LEDFeedback("Toggle4", ledOFF);
                ToggleStatus[3] = false;
                break;
            case "Lost-Toggle5":
                // turn off LED feedback
                LEDFeedback("Toggle5", ledOFF);
                ToggleStatus[4] = false;
                break;
            case "Got-CoverToggle1":
                // turn on LED feedback
                LEDFeedback("CoverToggle1", ledON);
                CoverToggleStatus[0] = true;
                break;
            case "Got-CoverToggle2":
                // turn on LED feedback
                LEDFeedback("CoverToggle2", ledON);
                CoverToggleStatus[1] = true;
                break;
            case "Got-CoverToggle3":
                // turn on LED feedback
                LEDFeedback("CoverToggle3", ledON);
                CoverToggleStatus[2] = true;
                cnt = 0;
                for(int i = 0; i < CoverToggleAns.Count; i++){
                    if(CoverToggleAns[i] == CoverToggleStatus[i]){
                        cnt += 1;
                    }
                }
                if(cnt == 2){
                    infos.text = "correct";
                    acWin.Play();
                }
                else{
                    infos.text = "incorrect";
                    wrongCnt += 1; 
                    if(wrongCnt == 1){
                        acLose.Play();
                        L1N.SetActive(false);
                        L1R.SetActive(true);
                        acLose.Play();
                    }
                    else{
                        L1N.SetActive(false); 
                        L2N.SetActive(false); 

                        L1R.SetActive(true); 
                        L2R.SetActive(true);
                        acAllLose.Play();
                    }
                }
                break;
            case "Lost-CoverToggle1":
                // turn off LED feedback
                LEDFeedback("CoverToggle1", ledOFF);
                CoverToggleStatus[0] = false;
                break;
            case "Lost-CoverToggle2":
                // turn off LED feedback
                LEDFeedback("CoverToggle2", ledOFF);
                CoverToggleStatus[1] = false;
                break;
            case "Lost-CoverToggle3":
                // turn off LED feedback
                LEDFeedback("CoverToggle3", ledOFF);
                CoverToggleStatus[2] = false;
                break;
            case "Got-Key":

                break;
            case "Lost-Key":

                break;
            case "Got-AlClip1":
                // turn on LED feedback
                LEDFeedback("AlClip1", ledON);
                ClipStatus[0] = true;
                break;
            case "Got-AlClip2":
                // turn on LED feedback
                LEDFeedback("AlClip2", ledON);
                ClipStatus[1] = true;
                break;
            case "Got-AlClip3":
                // turn on LED feedback
                LEDFeedback("AlClip3", ledON);
                ClipStatus[2] = true;
                break;
            case "Got-AlClip4":
                // turn on LED feedback
                LEDFeedback("AlClip4", ledON);
                ClipStatus[3] = true;
                break;
            case "Lost-AlClip1":
                // turn off LED feedback
                LEDFeedback("AlClip1", ledOFF);
                ClipStatus[0] = false;
                break;
            case "Lost-AlClip2":
                // turn off LED feedback
                LEDFeedback("AlClip2", ledOFF);
                ClipStatus[1] = false;
                break;
            case "Lost-AlClip3":
                // turn off LED feedback
                LEDFeedback("AlClip3", ledOFF);
                ClipStatus[2] = false;
                break;
            case "Lost-AlClip4":
                // turn off LED feedback
                LEDFeedback("AlClip4", ledOFF);
                ClipStatus[3] = false;
                break;
            case "Got-Knife1":
                // turn on LED feedback
                LEDFeedback("Knife1", ledON);
                KnifeStatus[0] = true;
                if((ClipStatus[0]) && (ClipStatus[2])){
                    //Debug.Log("correct1 correct");
                    correctnessStatus.text = "correct1 correct";
                    acWin.Play();
                    Debug.Log("!!!!!!!!!!");
                }
                else{
                    wrongCnt += 1; 
                    if(wrongCnt == 1){
                        acLose.Play();
                        L1N.SetActive(false);
                        L1R.SetActive(true);
                        acLose.Play();
                    }
                    else{
                        L1N.SetActive(false); 
                        L2N.SetActive(false); 

                        L1R.SetActive(true); 
                        L2R.SetActive(true);
                        acAllLose.Play();
                    }
                }
                break;
            case "Got-Knife2":
                // turn on LED feedback
                LEDFeedback("Knife2", ledON);
                KnifeStatus[1] = true;
                if(((ClipStatus[0]) && ClipStatus[1]) && (ClipStatus[2] && ClipStatus[3])){
                    correctnessStatus.text = "correct2 correct";
                    Debug.Log("@@@@@@@@@@@@");
                    acWin.Play();
                }
                else{
                    wrongCnt += 1; 
                    if(wrongCnt == 1){
                        acLose.Play();
                        L1N.SetActive(false);
                        L1R.SetActive(true);
                        acLose.Play();
                    }
                    else{
                        L1N.SetActive(false); 
                        L2N.SetActive(false); 

                        L1R.SetActive(true); 
                        L2R.SetActive(true);
                        acAllLose.Play();
                    }
                }
                break;
            case "Got-Knife3":
                // turn on LED feedback
                LEDFeedback("Knife3", ledON);
                KnifeStatus[2] = true;
                if(((ClipStatus[0]) && ClipStatus[1]) && (ClipStatus[2] && ClipStatus[3])){
                    correctnessStatus.text = "correct3 correct";
                    Debug.Log("@@@@@@@@@@@@");
                    acWin.Play();
                }
                else{
                    wrongCnt += 1; 
                    if(wrongCnt == 1){
                        acLose.Play();
                        L1N.SetActive(false);
                        L1R.SetActive(true);
                        acLose.Play();
                    }
                    else{
                        L1N.SetActive(false); 
                        L2N.SetActive(false); 

                        L1R.SetActive(true); 
                        L2R.SetActive(true);
                        acAllLose.Play();
                    }
                }
                break;
            case "Lost-Knife1":
                // turn off LED feedback
                LEDFeedback("Knife1", ledOFF);
                mt.mtKnifeLose(0);
                break;
            case "Lost-Knife2":
                // turn off LED feedback
                LEDFeedback("Knife2", ledOFF);
                mt.mtKnifeLose(1);
                break;
            case "Lost-Knife3":
                // turn off LED feedback
                LEDFeedback("Knife3", ledOFF);
                mt.mtKnifeLose(2);
                break;
            case "Got-cirTouchTouch":
                // when you receive a touch event you dont get the value so request it
                osc.SendOSCMessage("/phidget cirTouch getSensor");
                // turn on LED feedback
                LEDFeedback("CirTouch", ledON);

                break;
            case "Lost-cirTouchTouch":
                // turn off LED feedback
                LEDFeedback("CirTouch", ledOFF);

                break;
            case "Got-cirTouchNear":

                break;
            case "Lost-cirTouchNear":

                break;
            case "Got-circularTouch0":

                break;
            case "Got-circularTouch1":

                break;
            case "Got-circularTouch2":

                break;
            case "Got-circularTouch3":

                break;
            case "Got-circularTouch4":

                break;
            case "Got-circularTouch5":

                break;
            case "Got-circularTouch6":

                break;
            case "Got-circularTouch7":

                break;
            case "Got-circularTouch8":

                break;
            case "Got-circularTouch9":

                break;
        
            ////////////////////// UTILITY ///////////////////////////////////////////////
            case "On-Panel1":
                // turn on LED feedback for Panel1
                LEDFeedback("Panel1", ledON);
                break;
            case "Off-Panel1":
                // turn off LED feedback for Panel1
                LEDFeedback("Panel1", ledOFF);
                break;
            case "On-Panel2":
                // turn on LED feedback for Panel2
                LEDFeedback("Panel2", ledON);
                break;
            case "Off-Panel2":
                // turn off LED feedback for Panel2
                LEDFeedback("Panel2", ledOFF);
                break;
            case "On-Panel3":
                // turn on LED feedback for Panel3
                LEDFeedback("Panel3", ledON);
                break;
            case "Off-Panel3":
                // turn off LED feedback for Panel3
                LEDFeedback("Panel3", ledOFF);
                break;
            case "On-LeftLight":
                // turn on LED feedback for LeftLight
                LEDFeedback("LeftLight", ledON);
                break;
            case "Off-LeftLight":
                // turn off LED feedback for LeftLight
                LEDFeedback("LeftLight", ledOFF);
                break;
            case "On-RightLight":
                // turn on LED feedback for RightLight
                LEDFeedback("RightLight", ledON);
                break;
            case "Off-RightLight":
                // turn off LED feedback for RightLight
                LEDFeedback("RightLight", ledOFF);
                break;

                ////////////////////// END CASES ///////////////////////////////////////////////
        }
    }

    void LEDFeedback(string tag, int value) {
        osc.SendOSCMessage("/phidget LED setTag " + tag + " " + value.ToString());
    }


}
