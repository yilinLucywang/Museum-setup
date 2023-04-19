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
    public List<GameObject> tFails = new List<GameObject>();
    public List<GameObject> tSuccesses = new List<GameObject>();public Text infos;
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


        for(int i = 0; i < 3; i++){
            tSuccesses[i].SetActive(false);
            tFails[i].SetActive(true);
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
        tSuccesses[idx].SetActive(true);
        tFails[idx].SetActive(false);
    }

    public void lostCToggles(int idx){
        toggles[idx] = false;
        tSuccesses[idx].SetActive(false);
        tFails[idx].SetActive(true);
    }

    public void jumpToCWrongAns(){
        infos.text = "The answer is incorrect!";
    }

    public void jumpToCRightAns(){
        infos.text = "The answer is correct!"; 
    }
}
