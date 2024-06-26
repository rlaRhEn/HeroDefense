using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    MoveMent2D movement2D;
    Transform target;
    float damage;
    float speed = 5;
    [SerializeField] Transform head;


    public void Setup(Transform target,float damage)
    {
        movement2D = GetComponent<MoveMent2D>();
        this.target = target;
        this.damage = damage;
    }
    private void Update()
    {
        if(target != null)//타겟이 있으면
        {
            //타겟을 향해 날라감
            //Vector3 direction = (target.position - transform.position).normalized;
            
            RotateToTarget();
        }
        else 
        {
            this.gameObject.SetActive(false);
        }
    }
    private void RotateToTarget() //수정
    {
        // 타겟 방향 계산
        Vector3 direction = (target.position -  transform.position).normalized;
        movement2D.MoveTo(direction);
        // 화살의 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)) ;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return; //적이 아닌 대상과 부딪히면
        if (collision.transform != target) return;  //현재 target인 적이 아닐 때

        //collision.GetComponent<MonsterCon>().OnDieMonster(); // 적 사망 함수 호출
        Vector3 pos = Camera.main.WorldToScreenPoint(collision.transform.position);
        collision.GetComponent<MonsterCon>().TakeDamage(pos, damage);
        
        gameObject.SetActive(false);
    }
}
