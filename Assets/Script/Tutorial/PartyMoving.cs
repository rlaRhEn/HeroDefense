using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMoving : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] SPUM_Prefabs[] spum_Prefabs;
    RectTransform[] rectTransform;
    [SerializeField] float attackTimer, attackTerm = 3;

    public enum UnitState
    {
        idle,
        run,
        attack,
        skill,
        sturn,
        death

    }
    public UnitState _unitState = UnitState.idle;
    


    private void Awake()
    {
        spum_Prefabs = GetComponentsInChildren<SPUM_Prefabs>(); //자식 프리팹 스크립트 가져오기 애니메이션 사용 용도
        rectTransform = GetComponentsInChildren<RectTransform>(); //자식 위치정보 가져오기 
        SetPartyList();
    }
    void Update()
    {
        //CheckState();
        PartyMove();
        //attackTimer += Time.deltaTime; // 공격 애니메이션
        //if (attackTimer >= attackTerm) //현재 문제 애니메이션 한개를 하면 다른 한개 작동 x
        //{
        //    PartyAniCheck("2_Attack_Normal");
        //    attackTimer = 0;
        //    Debug.Log("애니메이션 작동");
        //}
    }
    void SetPartyList()
    {
        
    }
    public void CheckState()
    {
        switch(_unitState)
        {
            case UnitState.idle:
                break;
            case UnitState.run:
                //PartyMove();
                break;
            case UnitState.attack:
                break;
            case UnitState.skill:
                break;
            case UnitState.sturn:
                break;
            case UnitState.death:
                break;
        }
    }
        public void PartyMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (x > 0)
        {
            FlipX(true);
        }
        else if(x < 0) FlipX(false);
        if (x != 0 || y != 0)
        {
            Vector3 moveVelocity = new Vector3(x, y, 0) * speed * Time.deltaTime;
            this.transform.position += moveVelocity;
            PartyAniCheck("1_Run");
        }
        else if (x == 0 && y == 0) //멈춰 있을 경우
        {
            PartyAniCheck("0_idle");
        }
    }
    public void PartyAniCheck(string aniName)//캐릭터 애니메이션 동작
    {
        for (int i = 0; i < transform.childCount; i++) // i= 파티원 수
        {
            spum_Prefabs[i].PlayAnimation(aniName);
        }
    }
    public void FlipX(bool flipX) // 캐릭터 좌,우 반전 ※ 어색하면 수정 필요
    {
        for(int i = 0; i < transform.childCount; i++)
        if(flipX)// x가 증가하면 오른쪽으로보기
        {
            rectTransform[i].localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            rectTransform[i].localScale = new Vector3(1, 1, 1);
        }
    }
}
