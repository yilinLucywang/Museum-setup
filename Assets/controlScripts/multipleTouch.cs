using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=98dQBWUyy9M
public class multipleTouch : MonoBehaviour
{

    public GameObject circle; 
    public List<touchLocation> touches = new List<touchLocation>();
    public List<GameObject> targets = new List<GameObject>();
    private List<GameObject> circles = new List<GameObject>();
    private List<bool> matches = new List<bool>();
    private bool end = false; 
    public Text infos;
    private List<int> anses = new List<int>();
    private List<bool> ClipStatus = new List<bool>();
    private List<bool> KnifeStatus = new List<bool>();
    private bool correct1 = false;
    private bool correct2 = false;
    private bool correct3 = false;
    void Awake(){
        anses.Add(1); 
        anses.Add(2); 
        anses.Add(5); 
        for(int i = 0; i < 3; i++){
            ClipStatus.Add(false);
        }
        for(int i = 0; i < 3; i++){
            KnifeStatus.Add(false);
        }
    }
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

            if(KnifeStatus[0]){
                if(matches[3]){
                    if(ClipStatus[0] and ClipStatus[2]){
                        correct1 = true;
                    }
                }
            }
            else if(KnifeStatus[1]){
                if(matches[1] and matches[3]){
                    if((ClipStatus[0] and ClipStatus[1]) and (ClipStatus[2] and ClipStatus[3])){
                        correct2 = true;
                    }
                }

            } 
            else if(KnifeStatus[2]){
                int cnt = 0;
                for(int i = 0; i < matches.Count; i++){
                    if(matches[i]){
                        cnt += 1;
                    }
                }
                if(cnt == 5){
                    if(ClipStatus[0] and ClipStatus[3]){
                        correct3 = true;
                    }
                }
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


    public void mtClip(int index){
        ClipStatus[index] = true;
    }

    public void mtClipLose(int index){
        ClipStatus[index] = false;
    }


    public void mtKnife(int index){
        KnifeStatus[index] = true;
    }

    public void mtKnifeLose(int index){
        KnifeStatus[index] = false;
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
