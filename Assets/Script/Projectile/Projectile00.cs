using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile00 : Projectile
{
    [SerializeField] Transform head;
    private void Update()
    {
        if (target != null)//타겟이 있으면
        {
            //타겟을 향해 날라감
            //Vector3 direction = (target.position - transform.position).normalized;

            RotateToTarget();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    private void RotateToTarget() //수정
    {
        // 타겟 방향 계산
        Vector3 direction = (target.position - transform.position).normalized;
        movement2D.MoveTo(direction);
        // 화살의 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
