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
    public int radius = 50;
    public bool pressed = false;
    public int ctr = 0; 
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
    }


}
