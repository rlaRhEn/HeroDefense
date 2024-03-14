using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Entity
{ 
    protected override void Awake()
    {
        base.Awake();
       

    }
    public override void Update()
    {
        MovementLogic(Input.GetAxis("Horizontal"), Input.GetKeyUp(KeyCode.Space));
    }
     
}
