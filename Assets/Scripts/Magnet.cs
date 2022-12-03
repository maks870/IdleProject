using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Magnet : MonoBehaviour, IUpgradeble
{
    [SerializeField] private float speed;
    private CircleCollider2D col;
    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        SetDataVariables();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Magnetized")
        {
            Vector3 direct = Player.instance.transform.position - collision.transform.position;
            collision.transform.position += direct.normalized * Time.deltaTime * speed;
        }
    }
    public void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        col.radius = (upgrader.GetBaseValue("collectorRadius") * upgrader.GetValue("collectorRadius") / 100);
    }
    public void Upgrade(string statName)
    {
        YandexGame.savesData.collector.AddValue(statName, 1);
    }
    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.collector.GetValue(statName);
    }

}
