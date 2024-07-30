using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
   
    protected float damage;

    protected Transform target;
    protected MoveMent2D movement2D;

    public void Setup(Transform target,float damage)
    {
        movement2D = GetComponent<MoveMent2D>();
        this.target = target;
        this.damage = damage;
    }
  
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return; //적이 아닌 대상과 부딪히면
        if (collision.transform != target) return;  //현재 target인 적이 아닐 때

        //collision.GetComponent<MonsterCon>().OnDieMonster(); // 적 사망 함수 호출
        Vector3 pos = Camera.main.WorldToScreenPoint(collision.transform.position);
        collision.GetComponent<MonsterCon>().TakeDamage(pos, damage);
        
        gameObject.SetActive(false);
    }
}
