using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using System; 
using UnityEngine.SceneManagement;

public class RotationController : MonoBehaviour
{
    public GameObject edge;
    public GameObject center;
    public int radius = 207;
    public bool pressed = false;
    public int ctr = 0; 

    public GameObject fill; 
    public List<GameObject> ns;
    private int index = 0;

    public float barValue = 0; 
    public float decreaseRate = 0.02f;
    public float increaeRate = 0.10f;

    private Vector2 oriPos; 

    private float prevAngle = 0; 
    private float fullValue = 1f;
    private float ratio = 0.0f;
    private int cnt = 0;

    public AudioSource good; 
    public AudioSource bad; 
    public AudioSource allBad;
    public AudioSource win; 
    public AudioSource lose;
    public GameObject L1N; 
    public GameObject L1R; 

    public GameObject L2N; 
    public GameObject L2R;
    void Awake(){
        oriPos = new Vector2(edge.transform.position.x, edge.transform.position.y);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;;
        if(pressed){
            rotateObject(mousePosition);
        }
        //decreases the value
        if(barValue >= 0){
            barValue -= decreaseRate * Time.deltaTime;
        }
        Debug.Log(barValue);
        //update the value of the bar 
        barValue = Mathf.Min(barValue, fullValue);
        barValue = Mathf.Max(barValue, 0.0f);
        ratio = Mathf.Min(barValue/fullValue,1.0f);
        fill.transform.localScale = new Vector3(fill.transform.localScale.x, ratio, fill.transform.localScale.z);
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
            prevAngle = 0;
            barValue = 0;
        }
        else{
            float angleDiff = angle - prevAngle; 
            barValue += increaeRate * angleDiff; 
            prevAngle = angle; 
        }

    }

    public void submit1(){
        if(ratio > 0.25f){
            good.Play();
        }
        else{
            cnt += 1;
            ns[index].SetActive(false);
            index += 1;
            bad.Play();
        }
    }

    public void submit2(){
        if(ratio > 0.5f){
            good.Play();
        }
        else{
            cnt += 1;
            ns[index].SetActive(false);
            index = index + 1;
            bad.Play();
        }
    }

    IEnumerator WaitToNextLevel(){
        yield return new WaitForSeconds(25);
        SceneManager.LoadScene (2);
    }

    public void submit3(){
        if(ratio > 0.75f){
            good.Play();
        }
        else{
            cnt += 1;
            bad.Play();
        }
        if(cnt < 2){
            win.Play();
        }
        else{
            lose.Play();
        }
        StartCoroutine(WaitToNextLevel());
    }
}
