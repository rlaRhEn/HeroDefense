using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGS;

public class MoveMent2D : MonoBehaviour
{
    [SerializeField] Vector3 moveDirection = Vector3.zero;
    public float moveSpeed;

    void Start()
    { 

    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
