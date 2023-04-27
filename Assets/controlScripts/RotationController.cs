using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using System; 

public class RotationController : MonoBehaviour
{
    public GameObject edge;
    public GameObject center;
    public int radius = 207;
    public bool pressed = false;
    public int ctr = 0; 
    public Camera camera;

    public GameObject fill; 
    public Text infobox1;
    public Text infobox2;
    public Text infobox3;


    public float barValue = 0; 
    public float decreaseRate = 0.02f;
    public float increaeRate = 0.02f;

    private Vector2 oriPos; 

    private float prevAngle = 0; 
    private float fullValue = 1f;


    private List<float> ansValue = new List<float>();
    public List<bool> passStatus = new List<bool>();
    private List<bool> scoringStatus = new List<bool>();
    private bool gameEnd = false; 
    public AudioSource acWin; 
    public AudioSource acLose;
    void Awake(){
        oriPos = new Vector2(edge.transform.position.x, edge.transform.position.y);
        ansValue.Add(0.25f);
        ansValue.Add(0.50f); 
        ansValue.Add(0.75f);

        for(int i = 0; i < 3; i++){
            passStatus.Add(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z));
        if(pressed){
            rotateObject(mousePosition);
        }
        //decreases the value
        if(barValue > 0){
            barValue -= decreaseRate * Time.deltaTime;
        }

        //update the value of the bar 
        float ratio = barValue/fullValue;
        fill.transform.localScale = new Vector3(fill.transform.localScale.x, ratio, fill.transform.localScale.z);
        
        int ctr = 0; 
        for(int i = 0; i < 3; i++){
            if(passStatus[i]){
                ctr += 1;
            }
        }
        if(ctr == 3){
            gameEnd = true; 
        }

    }

    public void OnClick(){
        ctr += 1; 
        if(ctr == 1){
            pressed = true;
        }
        else if(ctr == 2){
            pressed = false; 
            ctr = 0;
        }
    }

    private void rotateObject(Vector3 mousePosition){
        Vector3 centerPos = center.transform.position;
        Vector3 diff = mousePosition - centerPos; 
        diff.Normalize(); 
        Vector3 finalPos = centerPos + diff * radius;
        edge.transform.position = finalPos;

        //vec2 (edge - center)
        Vector2 rotVec = new Vector2((float)finalPos.x, (float)finalPos.y) - new Vector2(center.transform.position.x, center.transform.position.y);
        //vec2 (ori - center)
        Vector2 normVec = new Vector2((float)oriPos.x, (float)oriPos.y) - new Vector2(center.transform.position.x, center.transform.position.y);
        //vec2 (ori - center)
        //cross product get the angle between those two vectors
            //1. if angle smaller, set value to 0
            //2. if angle larger, increase the value
        float angle = Vector2.Angle(normVec, rotVec);
        if(angle < prevAngle){
            barValue = 0f;
            prevAngle = angle;
        }
        else{
            float angleDiff = angle - prevAngle; 
            barValue += increaeRate * angleDiff; 
            prevAngle = angle; 
        }

    }


    public void getCToggles(int index){
        if(passStatus[index] == false){
            passStatus[index] = true;
            if(barValue >= ansValue[index]){
                scoringStatus[index] = true;
            }
            else{
                scoringStatus[index] = false; 
            }
        }
    }

    public void lostCToggles(int index){
    }


    public void submit1(){
        if(barValue >= ansValue[0]){
            scoringStatus[0] = true;
            infobox1.text = "sublevel one correct";
            acWin.Play();
        }
        else{
            infobox1.text = "sublevel one incorrect";
            acLose.Play();
        }
    }

    public void submit2(){
        if(barValue >= ansValue[1]){
            scoringStatus[1] = true;
            infobox2.text = "sublevel two correct";
            acWin.Play();
        }
        else{
            infobox2.text = "sublevel two incorrect";
            acLose.Play();
        }
    }

    public void submit3(){
        if(barValue >= ansValue[2]){
            scoringStatus[2] = true;
            infobox3.text = "sublevel three correct";
            acWin.Play();
        }
        else{
            infobox3.text = "sublevel three incorrect";
            acLose.Play();
        }
    }
}
