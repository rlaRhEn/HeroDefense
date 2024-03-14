using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGS;


public class MyParty : MonoBehaviour
{
    public PartyData partydata;
    //GameBalance.MyParty myParty;
   
    void Start()
    {
        var myParty = new GameBalance.MyParty();
        myParty.key = 200;
        myParty.name = "knight";
        myParty.hp = 180;
        myParty.attack = 10;
        myParty.armor = 11;
        myParty.attackspeed = 1;
        UnityGoogleSheet.Write<GameBalance.MyParty>(myParty);
        //파티 데이터 쓰기
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void Init(PartyData[] data)
    //{
    //    myParty.key = data.key;
    //}
}
[System.Serializable]
public class PartyData
{
    public int key;
}
