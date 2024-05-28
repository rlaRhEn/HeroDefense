using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UGS;

public class ShowText : MonoBehaviour
{
    [SerializeField] float limitTime;

    public Text timeText, goldText, waveText,monsterCountText;
    public Text hpText, armorText, typeText;

    [SerializeField] PlayerGold playerGold;
    [SerializeField] WaveSystem waveSystem;
    [SerializeField] MonsterSpawner monsterSpawner;

    private void Start()
    {
        StartCoroutine(Initialize());
    }
    private IEnumerator Initialize()
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        
    }
    private void Update()
    {
        CountDown();
        ShowGold();
        ShowMonsterCount();
        ShowWave();
        ShowMonsterInfo();
    }
    void ShowMonsterInfo()
    {
        var data = GameManager.instance.GetMonsterData(waveSystem.CurrentWave-1);
        if(data != null)
        {
            hpText.text = "Hp: " + data.hp;
            armorText.text = "Armor: " + data.armor;
            typeText.text = "Type: " + data.type;
        }
    }
    public void CountDown()
    {
        limitTime -= Time.deltaTime;
        timeText.text = "남은 시간: " + Mathf.Round(limitTime);
        if (limitTime <= 0) //필드에 몬스터가 남아있다면 게임패배
        {
            //GameOver();
        }
    }
    public void RoundClear()
    {
        limitTime = 90;
    }
    void ShowGold()
    {
        goldText.text = "Gold: " + playerGold.CurrentGold.ToString();
    }
    void ShowMonsterCount()
    {
        monsterCountText.text = "남아있는 적: " +monsterSpawner.currentEnemyCount.ToString();
    }
    void ShowWave()
    {
        waveText.text = "Wave: "+ waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
    }
   
}
