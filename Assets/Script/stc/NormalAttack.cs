using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : AttackTest
{
    public override void DoAttack()
    {
        spum_prefabs.PlayAnimation("2_Attack_Normal");
        spum_prefabs.PlayAnimation("0_idle");
       
    }
}
