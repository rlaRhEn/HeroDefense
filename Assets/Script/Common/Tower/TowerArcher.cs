using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArcher : TowerCon
{
    public override void DoAttacker()
    {
        spum_Prefabs.PlayAnimation(3);
    }
    public override Transform SetProjectile()
    {
        Transform projectile = GameManager.instance.pool.GetProJectile(0).transform; //직업당 공격 프로젝타일 바꿔야함
        projectile.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); //타겟 위치에서 소환
        return projectile;
    }
}
