using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : AttackTest
{

    public override void DoAttack()
    {
        spum_prefabs.PlayAnimation("2_Attack_Magic");
        spum_prefabs.PlayAnimation("0_idle");
        //SetProjectile();

    }
    public override void SetProjectile()
    {
        Instantiate(projectilePrefab, testMonster.transform.position, Quaternion.identity);
        
    }
}
