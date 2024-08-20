using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public int monsterCount, wave;
    [SerializeField]float spawnTime = 3;
    [SerializeField] Transform startPos;
    [SerializeField]ShowText showText;

    public List<int> monsterList;
    public int currentEnemyCount; //현재 웨이브에 남아있는 적 숫자 x -> 지금 필드의 총합 몬스터 수 
    Wave currentWave;

    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxMonsterCount;
    public void StartWave(Wave wave)
    {
        currentWave = wave;
        //현재 웨이브의 최대 적 숫자를 저장

        //currentEnemyCount += currentWave.maxMonsterCount;
        StartCoroutine("SpawnMonster");
    }
    IEnumerator SpawnMonster()
    {
        showText.RoundClear();
        monsterCount = 0;
        while(monsterCount < currentWave.maxMonsterCount)
        {
            GameObject monster = GameManager.instance.pool.GetMonster(currentWave.level);
            monsterCount++;
            monsterList.Add(monsterCount);
            yield return new WaitForSeconds(spawnTime);
        }
    }
    private void Update()
    {
        currentEnemyCount = monsterList.Count;
        if (currentEnemyCount > 100) //몹 초과 시
        {
            GameManager.instance.GameOver();
        }
    }
}
