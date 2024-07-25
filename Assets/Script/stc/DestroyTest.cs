using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTest : MonoBehaviour
{
    float destroyTime;
    void Update()
    {
        destroyTime += Time.deltaTime;
        if(destroyTime >= 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
