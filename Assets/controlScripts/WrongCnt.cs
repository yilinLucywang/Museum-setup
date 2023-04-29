using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WrongCnt
{
    //public static WrongCnt WrongCnt = null;              
    
    public static int wrongCnt = 0;
    public static void addCnt(){
       wrongCnt += 1;
   }
   //Awake is always called before any Start functions
   // void Awake()
   // {
   //     // //Check if instance already exists
   //     // if (WrongCnt == null)
           
   //     //     //if not, set instance to this
   //     //     WrongCnt = this;
       
   //     // //If instance already exists and it's not this:
   //     // else if (WrongCnt != this)
           
   //     //     //Then destroy this. This enforces our singleton pattern, 
   //     //     // meaning there can only ever be one instance of a GameManager.
   //     //     Destroy(gameObject);    
       
   //     // //Sets this to not be destroyed when reloading scene / Switching scenes
   //     // DontDestroyOnLoad(gameObject); // VERY IMPORTANT
       
   // }
}
