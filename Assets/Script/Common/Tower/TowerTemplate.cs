using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject //타워 레벨 디자인
{
    public GameObject towerPrefab;
    public Weapon[] weapon;

    [System.Serializable]
    public struct Weapon
    {
        public float attack;
        public float attackIncrease; // 공격력증가량
        public int cost; //업그레이드 비용
        public int probability; //업그레이드 성공확률
        public int fail;//업그레이드 실패확률
        public int sell; //타워 판매 시 획득 골드
    }
}
