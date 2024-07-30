using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMagic : TowerCon
{
    float destroyTime;
    public override void DoAttacker()
    {
        //SetProjectile();
        spum_Prefabs.PlayAnimation("2_Attack_Magic");
        spum_Prefabs.PlayAnimation("0_idle");


    }
    public override Transform SetProjectile()
    {
        Transform projectile = GameManager.instance.pool.GetProJectile(1).transform; //직업당 공격 프로젝타일 바꿔야함
        projectile.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z); //캐릭터 위치 소환
        return projectile;

    }

}

