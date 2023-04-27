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
    public Camera camera;
    public GameObject canvas;
    public AudioSource acWin; 
    public AudioSource acLose;
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

        for(int i = 0; i < 5; i++){
            Debug.Log(targets[i].transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("hihihih");
        if(!end){
            int i = 0;
            while(i < Input.touchCount){
                Touch t = Input.GetTouch(i); 
                if(t.phase == TouchPhase.Began){
                    Debug.Log("touch began");
                    //touches.Add(gameObject.AddComponent<touchLocation>(t.fingerId, createCircle(t)));
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
            
        }
    }

    private void checkPos(){
        for(int j = 0; j < touches.Count; j++){
            for(int i = 0; i < targets.Count; i++){
                if((Vector3.Distance(touches[j].circle.transform.position,targets[i].transform.position))<= 5){
                    matches[i] = true;
                }
            }
        }

        for(int i = 0; i < matches.Count; i++){
            Debug.Log(matches[i]);
        }
    }


    public void mtClip(int index){
        ClipStatus[index] = true;
    }

    public void mtClipLose(int index){
        ClipStatus[index] = false;
    }


    public void mtKnife(int index){
        infos.text = "=========";
        KnifeStatus[index] = true;
        if(index == 0){
            if((ClipStatus[0]) && (ClipStatus[2])){
                correct1 = true;
                //Debug.Log("correct1 correct");
                infos.text = "correct1 correct";
                Debug.Log("!!!!!!!!!!");
            }
        }
        else if(index == 1){
            if(((ClipStatus[0]) && ClipStatus[1]) && (ClipStatus[2] && ClipStatus[3])){
                correct2 = true;
                infos.text = "correct2 correct";
                Debug.Log("@@@@@@@@@@@@");
            }
        }
        else if(index == 2){
            if(ClipStatus[0] && ClipStatus[3]){
                correct3 = true;
                infos.text = "correct3 correct";
                Debug.Log("##############");
            }
        }
    }

    public void mtKnifeLose(int index){
        KnifeStatus[index] = false;
    }




    Vector2 getTouchPosition(Vector2 touchPosition){
        Vector3 pos = camera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 30.0f));
        return pos;
    }


    GameObject createCircle(Touch t){
        GameObject c = Instantiate(circle) as GameObject;
        c.name = "Touch" + t.fingerId; 
        //c.transform.position = getTouchPosition(t.position); 
        //Debug.Log(t.position);
        //c.transform.parent = canvas.transform;
        c.transform.position = getTouchPosition(t.position); 
        c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y, 30.0f);
        Debug.Log(c.transform.position); 
        circles.Add(c);
        return c;
    }
}
