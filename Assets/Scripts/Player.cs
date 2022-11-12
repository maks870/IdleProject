using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private int maxLevelCount;
    private int coins;
    private PlayerLevel level;
    public static Player instance = null;

    public int Coins { get => coins;}
    public PlayerLevel GetLevel { get => level; }

    protected override void Start()
    {
        base.Start();

        if (instance == null)
        {
            instance = this;
            level = new PlayerLevel(maxLevelCount);
        }
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
