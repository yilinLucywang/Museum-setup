using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WrongCnt
{
    public static int wrongCnt = 0; 

    public static void addCnt(){
        wrongCnt += 1;
    }
}
