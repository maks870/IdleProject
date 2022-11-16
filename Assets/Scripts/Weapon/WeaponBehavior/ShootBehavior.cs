using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(CircleCollider2D))]
public class ShootBehavior : Behavior
{
    [SerializeField] private List<StatsToImprove> statToImprove = new List<StatsToImprove>();
    [SerializeField] private GameObject projectile;
    [SerializeField] private CircleCollider2D shootZone;
    private List<Enemy> enemyList = new List<Enemy>();
    private void Start()
    {
        SetDataVariables();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemyList.Add(enemy);
            enemy.deathEvent.AddListener(delegate { enemyList.Remove(enemy); });
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemyList.Remove(enemy);
            enemy.deathEvent.RemoveListener(delegate { enemyList.Remove(enemy); });
        }
    }
    public override void Combine()
    {
        //������ ����������� ������
    }

    public override void ActiveBehavior()
    {

    }
    public override void Use()
    {
        if (enemyList.Count == 0)
        {
            return;
        }
        Enemy targetEnemy = enemyList[0];
        float distanse = (transform.position - targetEnemy.transform.position).magnitude;
        foreach (Enemy enemy in enemyList)
        {
            float newDistanse = (transform.position - enemy.transform.position).magnitude;
            if (newDistanse <= distanse)
            {
                targetEnemy = enemy;
                distanse = newDistanse;
            }
        }
        GameObject shoot = Instantiate(projectile, transform);
        shoot.GetComponent<Rigidbody2D>().AddForce((targetEnemy.transform.position - transform.position).normalized * projectile.GetComponent<Projectile>().speed , ForceMode2D.Impulse);
    }
    public override void Improve(bool isMaxLevel)
    {
        Debug.Log("�������� ��������");
        //����� ��������� ������
    }
    public override void SetDataVariables()
    {
        int improveLevel = YandexGame.savesData.shootWeaponLvl;
        projectile.GetComponent<Projectile>().damage = statToImprove[improveLevel].stats[0].value;
        shootZone.radius = statToImprove[improveLevel].stats[1].value;
    }
}
