using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageEnemy : Enemy
{
    [SerializeField] private float angleToRage = 90;
    private float baseSpeed;

    protected override void Start()
    {
        base.Start();
        baseSpeed = speed;
    }

    protected override void Update()
    {
        base.Update();
        float angleBetweenDirections = Vector3.Angle(-moveDirection, Player.instance.GetMoveDirection);

        if (angleBetweenDirections > angleToRage)
            speed = baseSpeed + Player.instance.Speed;
        else
            speed = baseSpeed;
    }
}
