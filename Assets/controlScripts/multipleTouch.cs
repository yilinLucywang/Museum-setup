using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=98dQBWUyy9M
public class multipleTouch : MonoBehaviour
{

    public GameObject circle; 
    public List<touchLocation> touches = new List<touchLocation>();
    public List<GameObject> targets = new List<GameObject>();
    public GameObject win; 
    public GameObject lose;
    private List<GameObject> circles = new List<GameObject>();
    private List<bool> matches = new List<bool>();
    private bool end = false; 
    void Start(){
        for(int i = 0; i < 5; i++){
            matches.Add(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!end){
            int i = 0;
            while(i < Input.touchCount){
                Touch t = Input.GetTouch(i); 
                if(t.phase == TouchPhase.Began){
                    Debug.Log("touch began");
                    touches.Add(new touchLocation(t.fingerId, createCircle(t)));
                }
                else if(t.phase == TouchPhase.Ended){
                    Debug.Log("touch ended");
                    touchLocation thisTouch = touches.Find(touchLocation=> touchLocation.touchId == t.fingerId);
                    Destroy(thisTouch.circle); 
                    touches.RemoveAt(touches.IndexOf(thisTouch));
                }
                else if(t.phase == TouchPhase.Moved){
                    Debug.Log("touch is moving");
                    touchLocation thisTouch = touches.Find(touchLocation=> touchLocation.touchId == t.fingerId);
                    thisTouch.circle.transform.position = getTouchPosition(t.position);
                }
                ++i;
            }
            checkPos();
            bool allMatch = true;
            for(int j = 0; j < matches.Count; j++){
                if(!matches[j]){
                    allMatch = false;
                }
            }
            if(allMatch){
                end = true;
                win.SetActive(true);
                lose.SetActive(false);
            }
        }
    }

    private void checkPos(){
        for(int j = 0; j < touches.Count; j++){
            for(int i = 0; i < targets.Count; i++){
                if((Vector3.Distance(touches[i].circle.transform.position,targets[i].transform.position))<= 0.3){
                    matches[i] = true;
                }
            }
        }

    }
    Vector2 getTouchPosition(Vector2 touchPosition){
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 0f));
    }


    GameObject createCircle(Touch t){
        GameObject c = Instantiate(circle) as GameObject;
        c.name = "Touch" + t.fingerId; 
        c.transform.position = getTouchPosition(t.position); 
        circles.Add(c);
        return c;
    }
}
