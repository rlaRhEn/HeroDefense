using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerKnight :TowerCon
{
    public override void DoAttacker()
    {
        spum_Prefabs.PlayAnimation("2_Attack_Normal");
        spum_Prefabs.PlayAnimation("0_idle");
    }
    public override Transform SetProjectile()
    {
        Transform projectile = GameManager.instance.pool.GetProJectile(2).transform; //직업당 공격 프로젝타일 바꿔야함
        projectile.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); //타겟 위치에서 소환
        return projectile;
    }
}
