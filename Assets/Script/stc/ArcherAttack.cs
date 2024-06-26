using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : AttackTest
{
    public override void DoAttack()
    {
        spum_prefabs.PlayAnimation("2_Attack_Bow");
        spum_prefabs.PlayAnimation("0_idle");
    }
}
