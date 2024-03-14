using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterInfo {Normal0 =0, Normal1 = 1, Normal2 = 2, Normal3 = 3, Normal4 = 4, Forest0 = 5, Forest1 = 6, Forest2 = 7, Forest3 = 8, Forest4 = 9 }
public class Character
{
    public static void CharacterNum(CharacterInfo characterInfo, int number)
    {
        //캐릭터 정보를 int형 변환
        number = (int)characterInfo; 
    }
}
