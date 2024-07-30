using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile02 : Projectile
{
    private void Update()
    {
        if (target != null)//타겟이 있으면
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
}
