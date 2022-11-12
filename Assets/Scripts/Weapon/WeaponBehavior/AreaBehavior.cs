using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AreaBehavior : WeaponBehavior
{
    [SerializeField] private int damage;
    [SerializeField] private float areaSize;
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    private Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            enemyList.Add(collision.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            enemyList.Remove(collision.GetComponent<Enemy>());
        }
    }
    public override void CombineWeapon()
    {
        //логика объединения оружия
    }
    public override void UseBehavior()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].TakeDamage(damage);  
        }
        animator.SetTrigger("Use");
    }
    public void SetAreaSize(float newAreaSize)
    {
        areaSize = newAreaSize;
        transform.localScale = Vector3.one * areaSize;
    }

    void Start()
    {
        SetAreaSize(areaSize);
        animator = GetComponent<Animator>();
    }


    void Update()
    {

    }
}
