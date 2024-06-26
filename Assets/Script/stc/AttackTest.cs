using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  AttackTest : MonoBehaviour
{
    protected SPUM_Prefabs spum_prefabs;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected GameObject testMonster;


    [SerializeField] private float attackTimer;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackSpeed = 0.8f;
    
    


    public enum Unit_State
    {
        idle,
        attack
    }
    public Unit_State unit_State = Unit_State.idle;

    void CheckState() // 각 상태일 때 함수 실행
    {
        switch (unit_State)
        {
            case Unit_State.idle:
                FindMonster();
                break;
            case Unit_State.attack:
                CheckAttack();
                break;
        }
    }
    void SetState(Unit_State state) //다른 함수에서 setState 불러와 애니메이션 행동
    {
        unit_State = state;
        switch(unit_State)
        {
            case Unit_State.idle:
               
                spum_prefabs.PlayAnimation("0_idle");
                break;
            case Unit_State.attack:
                spum_prefabs.PlayAnimation("0_idle");
               
                break;
        }
    }
    private void Awake()
    {
        spum_prefabs = GetComponent<SPUM_Prefabs>();
    }
    private void Update()
    {
        CheckState();
    }

    public void FindMonster()
    {
        float closestDisSqr = Mathf.Infinity;
        Transform closestTarget = null;
        //사거리 안에 들어오면 캐릭터 공격 모션
        float distance = Vector2.Distance(testMonster.transform.position, transform.position);
        if(distance <= attackRange && distance <= closestDisSqr)
        {
            closestDisSqr = distance;
            closestTarget = testMonster.transform;
            SetState(Unit_State.attack);
            return;
        }

    }
    public void CheckAttack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackSpeed)
        {
            DoAttack(); //애니메이션
            attackTimer = 0;
        }
    }
    public virtual void DoAttack() //공격 애니메이션 attackNormal 실행 3초뒤 idle 3초 뒤 attackNormal 다시 공격 모션하는데 총 6초 ;
    { 
        //spum_prefabs.PlayAnimation("2_Attack_Normal");
        //spum_prefabs.PlayAnimation("0_idle");
        //SetProjectile();
    }
    public virtual void SetProjectile() //공격 무기
    {

    }
   
}
