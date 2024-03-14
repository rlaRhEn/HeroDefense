using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGS;


public class MonsterManager : MonoBehaviour
{
    [SerializeField] int monsterKey, hp;
    [SerializeField] float attack, armor, attackSpeed;

    private void Start()
    {
        UnityGoogleSheet.Load<GameBalance.Monster>(); //구글 데이터 로드
        //foreach (var value in GameBalance.Monster.MonsterList) // 게임밸런스Job시트 데이터 불러오기
        //{
        //    //Debug.Log($"Loaded {value.key} {value.attack} {value.armor}"); // 키 공격 아머
        //}
        hp = GameBalance.Monster.MonsterMap[monsterKey].hp;
        attack = GameBalance.Monster.MonsterMap[monsterKey].attack;
        armor = GameBalance.Monster.MonsterMap[monsterKey].armor;
        attackSpeed = GameBalance.Monster.MonsterMap[monsterKey].attackSpeed;
    }
    public void DownHp()
    {
        hp -= 10;
        Debug.Log(hp);
    }
}
