using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UGS;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] MonsterSpawner monsterSpawner;
    public ObjectPool pool;

    [SerializeField] GameObject victory, fail;

    public Text nextWaveText;
    public Dictionary<int, monsterBal.Data> monsterData;
    [SerializeField] private float roundTimer;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
        roundTimer = 10;
    }
    private void Update()
    {
        NextWaveTimer();


    }
    public void NextWaveTimer()
    {
        nextWaveText.text = " ";
        if(monsterSpawner.monsterCount == 0)
        {
            roundTimer -= Time.deltaTime;
            nextWaveText.text = "Next Wave: " + Mathf.Round(roundTimer);
            if (roundTimer < 0)
            {
                GameObject.Find("PoolManager").GetComponent<WaveSystem>().StartWave(); //NextWave
                roundTimer = 10;
            }
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

    public void GameOver()
    {
        fail.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("게임오버");
    }
    public void GameClear()
    {
        victory.SetActive(true);
        Time.timeScale = 0;
    }

}
