using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    MoveMent2D movement2D;
    Transform target;
    float damage;

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
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else 
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return; //적이 아닌 대상과 부딪히면
        if (collision.transform != target) return;  //현재 target인 적이 아닐 때
       
        //collision.GetComponent<MonsterCon>().OnDieMonster(); // 적 사망 함수 호출
        collision.GetComponent<MonsterCon>().TakeDamage(damage);
        
        gameObject.SetActive(false);
    }
}
