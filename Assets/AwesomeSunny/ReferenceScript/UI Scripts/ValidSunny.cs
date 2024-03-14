using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidSunny : MonoBehaviour
{
    public List<DemoChecker> valids= new List<DemoChecker>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var value in valids)
        {
            if (value.valid == false)
            {
                this.GetComponent<CanvasGroup>().alpha = 1;
                return;
            }
        }

        Destroy(this.gameObject);
    }
}
