using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private int coins;
    public static Player instance = null;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coin>() != null)
            coins += collision.GetComponent<Coin>().GetValue();
    }

    protected override void Dead()
    {
        base.Dead();
        Debug.Log("You dead");
    }
}
