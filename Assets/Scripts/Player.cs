using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");
        moveDirection = transform.TransformDirection(moveDirection.normalized);
    }
}
