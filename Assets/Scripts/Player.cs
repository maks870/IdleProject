using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private int maxLevelCount;
    private int coins;
    private PlayerLevel level;
    public static Player instance = null;

    public int Coins => coins;
    public PlayerLevel GetLevel => level;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        level = new PlayerLevel(maxLevelCount);
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
    }
}
