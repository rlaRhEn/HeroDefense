using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UGS;
using TMPro;

public abstract class TowerCon : MonoBehaviour 
{

    public SPUM_Prefabs spum_Prefabs;
    [SerializeField] TowerTemplate towerTemplate;
    [SerializeField] PlayerGold playerGold;
    [SerializeField] TmpFadeInOut tmpFadeinout;
    [Header("Stat")]
    [SerializeField] int characterCode, level;
    [SerializeField] float attackSpeed, attackTimer;
    [SerializeField] string type;
    [SerializeField] float attackRange;// 현재 타겟

    [SerializeField]protected Transform target;

    [SerializeField]Tile ownerTile;

    public float Attack => towerTemplate.weapon[level].attack;
    public float AttackIncrease => towerTemplate.weapon[level].attackIncrease;
    public int Cost => towerTemplate.weapon[level].cost;
    public float Probablilty => towerTemplate.weapon[level].probability;
    public float Fail => towerTemplate.weapon[level].fail;

    public float Range => attackRange;
    public int Level => level+1;
    public int MaxLevel => towerTemplate.weapon.Length;

    public Vector3 goalPos;


    public enum TowerState
    {
        idle,
        attack
    }
    public TowerState tower_State;


    private void Awake()
    {
        tmpFadeinout = GameObject.Find("TmpFadeInOut").GetComponent<TmpFadeInOut>();
        spum_Prefabs = GetComponent<SPUM_Prefabs>();
        UnityGoogleSheet.Load<characterBal.Balance>();
    }
    void Start()
    {
        playerGold = GameObject.Find("GameManager").GetComponent<PlayerGold>();

        attackSpeed = 0.6f;
        //attackSpeed = characterBal.Balance.BalanceMap[characterCode].attackSpeed; //고정
        attackRange = characterBal.Balance.BalanceMap[characterCode].attackRange; //고정
        type = characterBal.Balance.BalanceMap[characterCode].type; //고정
        level = 0;

    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.localPosition.y * 0.01f);
        CheckState();
    }
    public void SetUp(Tile ownerTile) //타일 선택
    {
        this.ownerTile = ownerTile;
    }
    void CheckState(TowerState state = TowerState.idle) //수정
    {
        tower_State = state;
        switch (tower_State)
        {
            case TowerState.idle:
                SearchTarget();
                break;
            case TowerState.attack:
                AttackToTarget();
                break;
            
        }
    }

  

    public void SearchTarget()
    {
            // 제일 가까이 있는 적을 찾기 위해 최초 거리를 최대한 크게 설정
        float closestDisSqr = Mathf.Infinity;
        Transform closestTarget = null;

        for (int i = 0; i < GameManager.instance.pool.monsterPools.Length; i++)
        {
            // 몬스터 풀의 각 몬스터를 가져와서 transform 속성을 사용
            foreach (GameObject monster in GameManager.instance.pool.monsterPools[i])
            {
                if (monster != null && monster.CompareTag("Monster"))
                {
                    float distance = Vector3.Distance(monster.transform.position, transform.position);
                    if (distance <= attackRange && distance <= closestDisSqr)
                    {
                        closestDisSqr = distance;
                        closestTarget = monster.transform;
                    }
                }
            }
        }
        // 가장 가까운 적을 찾았으면 상태 변경
        if (closestTarget != null)
        {
            target = closestTarget;
            CheckState(TowerState.attack);
        }
    }
    public void AttackToTarget()
    {
            //타겟이 있는지 검사
            if (target == null || !target.gameObject.activeSelf)
            {
                Debug.Log("타겟 서치");
                CheckState(TowerState.idle);
            attackTimer = 0;
            }
            //타겟이 공격 범위 안에있는지 검사 (공격 범위 벗어나면 새로운 적 탐색)
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance > attackRange)
            {
                target = null;
                CheckState(TowerState.attack);
            }
            attackTimer += Time.deltaTime;
            if(attackTimer > attackSpeed)
            { 
                DoAttacker(); //공격 애니메이션
                Transform projectile = SetProjectile();//   
                projectile.GetComponent<Projectile>().Setup(target, towerTemplate.weapon[level].attack); //캐릭터 공격력
                attackTimer = 0;
            }

    }
    public virtual void DoAttacker()
    {
        spum_Prefabs.PlayAnimation("2_Attack_Normal");
        spum_Prefabs.PlayAnimation("0_idle");
    }
    
    public virtual Transform SetProjectile()
    { //직업당 공격 프로젝타일 바꿔야함


        return null;
    }
  
    public bool Upgrade() //강화
    {
        //타워 업그레이드에 필요한 골드가 충분한지 검사
        if (playerGold.CurrentGold < towerTemplate.weapon[level+1].cost)
        {
            //돈 부족
            return false;
        }
        playerGold.CurrentGold -= towerTemplate.weapon[level].cost;
        //확률 넣기   //타워레벨 증가
        if (TrySuccess())
        {
            level++;
            Invoke("Success",1f);
            //성공 사운드
            Debug.Log("강화성공");
            return true;
        }
        else // 10레벨 이상일 시 레벨 하락
        {
            if (level > 0)
                level--;
            Invoke("Failed", 1f);
            //실패 사운드
            Debug.Log("강화 실패");
            //Debug.Log(level);
            return false;

        }
    }
    public void Success()
    {
        StartCoroutine(tmpFadeinout.SuccessFadeInAndOutText());
    
        //성공 이미지 띄우기
    }
    public void Failed()
    {
        StartCoroutine(tmpFadeinout.FailedFadeInAndOutText());
        //실패 이미지 띄우기
    }

    public bool TrySuccess()
    {

        float randomValue = Random.value; // 0.0에서 1.0사이의 난수를 생성
        return randomValue < towerTemplate.weapon[level+1].probability / 100; //레벨당 확률로 true 변화
    }

    public void Sell()
    {
        //골드 증가
        playerGold.CurrentGold += towerTemplate.weapon[level].sell;
        ownerTile.IsBuildTower = false;
        Destroy(gameObject);
    }
}


