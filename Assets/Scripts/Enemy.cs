using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private GameObject target;

    public void SetTarget(GameObject value)
    { 
        target = value;
    }
    private new void Update()
    {
        base.Update();
        moveDirection = target.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
    }
}
