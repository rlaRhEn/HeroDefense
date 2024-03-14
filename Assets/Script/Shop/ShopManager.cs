using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //public int money;
    [SerializeField]
    CharacterInfo characterInfo;
    [SerializeField]
    Button refreshBtn;
    private void Update()
    {
        
    }

    public void OnClickRefresh()
    {
        int num = Random.Range(1, 10);
        Debug.Log(num);
        Character.CharacterNum(characterInfo, num);
    }
}
