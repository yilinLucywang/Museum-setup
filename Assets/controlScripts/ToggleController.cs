using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public int sceneIndex = 1;
    public List<bool> toggles = new List<bool>();
    //temporarily set to 1110
    public List<bool> answer = new List<bool>();
    public GameObject win; 
    public GameObject lose;
    void Awake(){
        for(int i = 0; i < 5; i++){
            toggles.Add(false);
        }
        for(int i = 0; i < 3; i++){
            answer.Add(true);
        }
        for(int i = 3; i < 4; i++){
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
        if(toggles[4]){
            bool isMatch = true;
            for(int i = 0; i < 4; i++){
                if(toggles[i] != answer[i]){
                    jumpToWrongAns();
                    isMatch = false;
                    break;
                }
            }
            if(isMatch){
                jumpToRightAns();
            }

        }
    }

    public void getToggles(int idx){
        toggles[idx] = true;
    }

    public void lostToggles(int idx){
        toggles[idx] = false;
    }

    public void jumpToWrongAns(){
        win.SetActive(false);
        lose.SetActive(true);
    }

    public void jumpToRightAns(){
        win.SetActive(true); 
        lose.SetActive(false); 
        SceneManager.LoadScene (sceneIndex + 1);
    }

}
