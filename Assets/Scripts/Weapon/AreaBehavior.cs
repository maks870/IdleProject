using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBehavior : WeaponBehavior
{
    [SerializeField] private int damage;
    [SerializeField] private float areaSize;
    private CircleCollider2D area;
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    private GameObject weaponAreaSprite;
    private Vector2 spiteSize;

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
        area.radius = areaSize;
        weaponAreaSprite.transform.localScale = Vector3.one * areaSize;
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponAreaSprite = transform.GetChild(0).gameObject;
        area = GetComponent<CircleCollider2D>();
        area.radius = areaSize;
        weaponAreaSprite.transform.localScale = Vector3.one * areaSize;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
