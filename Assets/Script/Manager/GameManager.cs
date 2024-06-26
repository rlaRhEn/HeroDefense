using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UGS;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public ObjectPool pool;

    public Dictionary<int, monsterBal.Data> monsterData;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void LoadData()
    {
        UnityGoogleSheet.Load<monsterBal.Data>();
        monsterData = monsterBal.Data.DataMap;
    }
   public monsterBal.Data GetMonsterData(int monsterCode)
    {
        if(monsterData.ContainsKey(monsterCode))
        {
            return monsterData[monsterCode];
        }
        return null;
    }
}
