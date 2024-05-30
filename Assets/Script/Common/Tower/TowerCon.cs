using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UGS;

public enum WeaponState { SearchTarget, AttackToTarget}
public class TowerCon : MonoBehaviour
{
    public SPUM_Prefabs spum_Prefabs;
    [SerializeField] TowerTemplate towerTemplate;
    [SerializeField] PlayerGold playerGold;
    [Header("Stat")]
    [SerializeField] int characterCode, level;
    [SerializeField] float attackSpeed, speed, attackTimer;
    [SerializeField] string type;
    [SerializeField] float attackRange;// 현재 타겟

    [SerializeField] Transform target;

    [SerializeField]Tile ownerTile;

    public float Attack => towerTemplate.weapon[level].attack;
    public float AttackIncrease => towerTemplate.weapon[level].attackIncrease;
    public int Cost => towerTemplate.weapon[level].cost;
    public float Probablilty => towerTemplate.weapon[level].probability;
    public int Fail => towerTemplate.weapon[level].fail;

    public float Range => attackRange;
    public int Level => level+1;
    public int MaxLevel => towerTemplate.weapon.Length;

    public Vector3 goalPos;


    public enum TowerState
    {
        idle,
        run,
        attack,
        skill,
        stun,
        death
    }
    public TowerState tower_State;
    public WeaponState weaponState = WeaponState.SearchTarget;

    private void Awake()
    {
        spum_Prefabs = GetComponent<SPUM_Prefabs>();
        UnityGoogleSheet.Load<characterBal.Balance>();
    }
    void Start()
    {
        playerGold = GameObject.Find("GameManager").GetComponent<PlayerGold>();
        attackSpeed = characterBal.Balance.BalanceMap[characterCode].attackSpeed;
        attackRange = characterBal.Balance.BalanceMap[characterCode].attackRange;
        type = characterBal.Balance.BalanceMap[characterCode].type;
        speed = characterBal.Balance.BalanceMap[characterCode].speed;
        level = 0;

    }
    private void OnEnable()
    {
        ChangeState(WeaponState.SearchTarget);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.localPosition.y * 0.01f);
        //CheckState();
    }
    public void SetUp(Tile ownerTile)
    {
        this.ownerTile = ownerTile;
    }
    public void ChangeState(WeaponState nowstate)//무기 state
    {
        //이전 재생중이던 상태 종료
        StopCoroutine(weaponState.ToString());
        //상태 변경
        weaponState = nowstate;
        //새로운 코루틴 실행
        StartCoroutine(weaponState.ToString());
    }
    void CheckState() //애니메이션 state
    {
        switch (tower_State)
        {
            case TowerState.idle:
                break;
            case TowerState.attack:
                break;
            case TowerState.run:
                DoMove();
                break;
            case TowerState.skill:
                break;
        }
    }

    public void SetState(TowerState state)
    {
        tower_State = state;
        switch (tower_State)
        {
            case TowerState.idle:
                spum_Prefabs.PlayAnimation("0_idle");
                break;
            case TowerState.attack:
                spum_Prefabs.PlayAnimation("2_Attack_Normal");
                break;
            case TowerState.run:
                spum_Prefabs.PlayAnimation("run");

                break;
            case TowerState.skill:
                spum_Prefabs.PlayAnimation("2_Attack_Magic");
                break;
        }
    }
   
    public void DoMove()
    {
        
        Vector3 _dirVec = goalPos - transform.position;
        Vector3 _disVec = (Vector2)goalPos - (Vector2)transform.position;
        if (_disVec.sqrMagnitude < 0.1f)
        {
            SetState(TowerState.idle);
            return;
        }
        Vector3 _dirMVec = _dirVec.normalized;
        transform.position += (_dirMVec * speed * Time.deltaTime);


        if (_dirMVec.x > 0) spum_Prefabs.transform.localScale = new Vector3(-1, 1, 1);
        else if (_dirMVec.x < 0) spum_Prefabs.transform.localScale = new Vector3(1, 1, 1);
    }
    public void SetMovePos(Vector2 pos)//목표지점이동
    {
        goalPos = pos;
        SetState(TowerState.run);
    }
    IEnumerator SearchTarget()
    {
        while (true)
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
                ChangeState(WeaponState.AttackToTarget);
            }

            yield return null;
        }
    }
    //IEnumerator SearchTarget()
    //{
    //    while(true)
    //    {
    //        제일 가까이 있는 적을 찾기 위해 최초거리를 최대한 크게설정
    //        float closesetDisSqr = Mathf.Infinity;

    //        for (int i = 0; i < GameManager.instance.pool.monsterPools.Length; i++)
    //        {
    //            GameObject monster = GameManager.instance.pool.monsterPools[0];
    //            Transform monsterTransform = monster.transform;
    //            float distance = Vector3.Distance(GameManager.instance.pool.monsterPools[i].transform.position, transform.position);
    //            if (distance <= attackRange && distance <= closesetDisSqr)
    //            {
    //                closesetDisSqr = distance;
    //                target = GameManager.instance.pool.monsterPools[i].transform;
    //            }
    //        }
    //        if(target != null)
    //        {
    //            ChangeState(WeaponState.AttackToTarget);
    //        }
    //        yield return null;
    //    }
    //}
    IEnumerator AttackToTarget()
    {
        while(true)
        {
            //spum_Prefabs.PlayAnimation("0_idle");
            spum_Prefabs.PlayAnimation("2_Attack_Normal");
            //타겟이 있는지 검사
            if (target ==null || !target.gameObject.activeSelf)
            {
                Debug.Log("타겟 서치");
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            //타겟이 공격 범위 안에있는지 검사 (공격 범위 벗어나면 새로운 적 탐색)
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if(distance > attackRange)
            {
                target = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            
            yield return new WaitForSeconds(attackSpeed);
            spum_Prefabs.PlayAnimation("2_Attack_Normal");
            Transform projectile =  GameManager.instance.pool.GetProJectile(0).transform;
            projectile.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            projectile.GetComponent<Projectile>().Setup(target, towerTemplate.weapon[level].attack);
            
        }
    }
    public bool Upgrade()
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
            Debug.Log("강화성공");
            return true;
        }
        else // 10레벨 이상일 시 레벨 하락
        {
            Debug.Log("강화 실패");
            return false;
        }
    }
    public bool TrySuccess()
    {
        float randomValue = Random.value; // 0.0에서 1.0사이의 난수를 생성
        Debug.Log(randomValue);
        return randomValue < towerTemplate.weapon[level + 1].probability / 100; //레벨당 확률로 true 변화
    }
    public void Sell()
    {
        //골드 증가
        playerGold.CurrentGold += towerTemplate.weapon[level].sell;
        ownerTile.IsBuildTower = false;
        Destroy(gameObject);
    }
}
