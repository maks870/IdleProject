using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character
{
    [SerializeField] private int maxLevelCount;
    private int coins;
    private Level level;
    public static Player instance = null;
    public UnityEvent endRound;

    public int Coins => coins;
    public Level GetLevel => level;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        level = new Level(maxLevelCount);
    }

    public void SetInputAxis(Vector2 axis) 
    {
        moveDirection = axis.normalized;
    }

    protected override void Update()
    {
        base.Update(); 
        moveDirection = transform.TransformDirection(moveDirection.normalized);

        if (moveDirection != Vector3.zero)
            animator.SetBool("Run", true);
        else
            animator.SetBool("Run", false);
    }

    public void AddCoin(int coinCount)
    {
        coins += coinCount;
    }

    protected override void Dead()
    {
        base.Dead();
        Debug.Log("You dead");
        endRound.Invoke();
        //MenuGame.instance.EndRound();
    }
}
