using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : AttackTest
{

    public override void DoAttack()
    {
        //SetProjectile();
        spum_prefabs.PlayAnimation("2_Attack_Magic");
        spum_prefabs.PlayAnimation("0_idle");
      

    }
    public override void SetProjectile()
    {
        Instantiate(projectilePrefab, testMonster.transform.position, Quaternion.identity);
        
    }
}
