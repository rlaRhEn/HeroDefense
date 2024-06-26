using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]Wave[] waves;
    [SerializeField] MonsterSpawner monsterSpawner;
    int currentWaveIndex = -1;
    //웨이브 정보 출력을 위한 get프로퍼티 (현재웨이브 ,총 웨이브)
    public int CurrentWave => currentWaveIndex + 1; //시작이 0이기 때문에 +1
    public int MaxWave => waves.Length;

    public void StartWave() //빠르게 진행하고 싶으면 여러번 시작 가능하게끔
    {
        if(currentWaveIndex < waves.Length-1)
        {
            //인덱스의 시작이 -1이기 때문에 웨이브 인덱스 증가를 제일먼저 함
            currentWaveIndex++;
            //현재 웨이브 정보 제공
            monsterSpawner.StartWave(waves[currentWaveIndex]);
        }
    }
}
[System.Serializable]
public struct Wave
{
    public int level; //레벨에 따른 적 등급 상승
    public int maxMonsterCount; // 강해지는 적 숫자 감소
}
