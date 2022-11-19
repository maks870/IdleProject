using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;

[RequireComponent(typeof(Upgrader))]
public class Player : Character, IUpgradeble
{
    [SerializeField] private int maxLevelCount;
    private Level level;
    public static Player instance = null;
    public UnityEvent endRound;

    public Level GetLevel => level;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
        SetDataVariables();
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

        if (moveDirection != Vector3.zero) animator.SetBool("Run", true);
        else animator.SetBool("Run", false);
    }

    protected override void Dead()
    {
        base.Dead();
        Debug.Log("You dead");
        endRound.Invoke();
    }

    public void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        hp = upgrader.GetDataVariable("health", YandexGame.savesData.playerSkill);
        speed = upgrader.GetDataVariable("speed", YandexGame.savesData.playerSkill);
    }
    public void Upgrade(string statName)
    {
        YandexGame.savesData.playerSkill[statName]++;
        YandexGame.SaveProgress();
    }
    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.playerSkill[statName];
    }
}
