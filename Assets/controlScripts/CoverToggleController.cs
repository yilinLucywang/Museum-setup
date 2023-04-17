using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CoverToggleController : MonoBehaviour
{
    public List<bool> toggles = new List<bool>();
    //temporarily set to 11
    public List<bool> answer = new List<bool>();
    public GameObject win; 
    public GameObject lose;
    void Awake(){
        for(int i = 0; i < 3; i++){
            toggles.Add(false);
        }
        for(int i = 0; i < 2; i++){
            answer.Add(true);
        }
        for(int i = 2; i < 3; i++){
            answer.Add(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(toggles[2]){
            bool isMatch = true;
            for(int i = 0; i < 2; i++){
                if(toggles[i] != answer[i]){
                    jumpToCWrongAns();
                    isMatch = false;
                    break;
                }
            }
            if(isMatch){
                jumpToCRightAns();
            }

        }
    }

    public void getCToggles(int idx){
        toggles[idx] = true;
    }

    public void lostCToggles(int idx){
        toggles[idx] = false;
    }

    public void jumpToCWrongAns(){
        win.SetActive(false);
        lose.SetActive(true);
    }

    public void jumpToCRightAns(){
        win.SetActive(true); 
        lose.SetActive(false); 
    }
}
