using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
public static class WrongCnt
{
    public static int wrongCnt = 0; 

    public static void addCnt(){
        wrongCnt += 1;
    }
}
