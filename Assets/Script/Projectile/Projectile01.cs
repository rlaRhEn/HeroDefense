using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile01 : Projectile
{
    private void Update()
    {
        if(target == null)
        {
            gameObject.SetActive(false);
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return; //적이 아닌 대상과 부딪히면
        if (collision.transform != target) return;  //현재 target인 적이 아닐 때

        //collision.GetComponent<MonsterCon>().OnDieMonster(); // 적 사망 함수 호출
        Vector3 pos = Camera.main.WorldToScreenPoint(collision.transform.position);
        collision.GetComponent<MonsterCon>().TakeDamage(pos, damage);
        Invoke("OnDestroyProjectile01", 0.5f);
        
    }
    public void OnDestroyProjectile01()
    {
        gameObject.SetActive(false);
    }
}
