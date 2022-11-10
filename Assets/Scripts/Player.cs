using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private int coins;
    public static Player instance = null;

    public int Coins { get => coins;}

    protected override void Start()
    {
        base.Start();

        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    protected override void Update()
    {
        base.Update();
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.y = Input.GetAxis("Vertical");
        moveDirection = transform.TransformDirection(moveDirection.normalized);
    }

    public void AddCoin(int coinCount) 
    {
        coins += coinCount;
    }
   
    protected override void Dead()
    {
        base.Dead();
        Debug.Log("You dead");
    }
}
