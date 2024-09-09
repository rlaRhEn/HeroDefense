using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile00 : Projectile //Bow
{
    private void Start()
    {
        speed = 7;
        yPos = 2;
    }
    private void Update()
    {
        AttackArrow();
    }
  
    private void AttackArrow() //자연스러운 공격
    {
        if(target != null)
        { 
            endPos = (Vector2)target.transform.position + new Vector2(0, 0.25f);

            Vector2 tVec = endPos - (Vector2)transform.position;

            float tDis = tVec.sqrMagnitude;
            if(tDis > 0.1f)
            {
                Vector2 tDirVec = (tVec).normalized;
                Vector3 tWect;
                if(yPos == -1f)
                {
                    tWect = (speed * (Vector3)tDirVec);

                }
                else
                {
                    yPos -= yPosSave * Time.deltaTime;
                    tWect = (speed * (Vector3)tDirVec + new Vector3(0, yPos, 0));
                }
                transform.position += tWect * Time.deltaTime;
                transform.up = tWect;
            }
        }
        else { gameObject.SetActive(false); }
    }
}
