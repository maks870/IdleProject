using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBehavior : WeaponBehavior
{
    [SerializeField] private int damage;
    [SerializeField] private float areaSize;
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();

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
    }
    public void SetAreaSize(float newAreaSize)
    {
        areaSize = newAreaSize;
        transform.localScale = Vector3.one * areaSize;
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
}
