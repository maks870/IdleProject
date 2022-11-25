using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

[RequireComponent(typeof(Upgrader))]
public class SpinBehavior : Behavior, IUpgradeble
{
    [SerializeField] private GameObject projectileObj;
    [SerializeField] private float spinningTime;
    [SerializeField] private float radius;
    private float projectileSpeed;
    private List<GameObject> spinList = new List<GameObject>();
    private List<Vector3> relativeDistanceList = new List<Vector3>();
    private GameObject spin;
    private Vector3 relativeDistance;
    private Vector3 offset;
    private bool isAddSpin = false;
    private bool isSpinActive = false;

    private void Awake()
    {
        SetDataVariables();
    }
    private void Start()
    {
        projectileSpeed = projectileObj.GetComponent<SpinProjectile>().speed;
    }

    void Update()
    {
        if (isSpinActive)
        {
            Spin();
        }
    }

    private void Spin()
    {
        for (int i = 0; i < spinList.Count; i++)
        {
            Quaternion rotate = Quaternion.Euler(0, 0, -projectileSpeed * Time.deltaTime);
            offset = (rotate * relativeDistanceList[i]).normalized;
            spinList[i].transform.position = transform.position + offset * radius;
            relativeDistanceList[i] = spinList[i].transform.position - transform.position;
        }
    }

    private void TurnOn()
    {
        foreach (GameObject spin in spinList)
        {
            spin.gameObject.SetActive(true);
        }
        isSpinActive = true;
    }

    private void TurnOff()
    {
        foreach (GameObject spin in spinList)
        {
            spin.gameObject.SetActive(false);
        }
        isSpinActive = false;
    }

    private void AddSpin()
    {
        int spinCounts = spinList.Count;
        spinCounts++;
        spinList.Clear();
        relativeDistanceList.Clear();
        for (int i = 0; i < spinCounts; i++)
        {
            float angle = i * Mathf.PI * 2f / spinCounts;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            spin = Instantiate(projectileObj, transform.position + newPos, Quaternion.identity);
            spinList.Add(spin);
            relativeDistance = spin.transform.position - transform.position;
            relativeDistanceList.Add(relativeDistance);
        }
        TurnOff();
        isAddSpin = false;
    }

    public override void Combine()
    {
        //������ ����������� ������
    }

    public override void ActiveBehavior()
    {
        AddSpin();
    }

    public override void Use()
    {
        StartCoroutine(SpinTimer());
    }

    public override void Improve(bool isMaxLevel)
    {
        if (!isMaxLevel)
        {
            if (isSpinActive)
            {
                isAddSpin = true;
            }
            else
            {
                AddSpin();
            }
        }
    }

    public void SetDataVariables()
    {
        Upgrader upgrader = GetComponent<Upgrader>();
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.damage = (int)(projectile.damage * upgrader.GetDataVariable("spinDamage", YandexGame.savesData.spinWeapon) / 100);
        radius *= upgrader.GetDataVariable("spinRadius", YandexGame.savesData.spinWeapon) / 100;
    }

    public void Upgrade(string statName)
    {
        YandexGame.savesData.spinWeapon[statName]++;
    }

    public int GetStatLvl(string statName)
    {
        return YandexGame.savesData.spinWeapon[statName];
    }

    IEnumerator SpinTimer()
    {
        TurnOn();
        yield return new WaitForSeconds(spinningTime);
        TurnOff();
        if (isAddSpin)
        {
            AddSpin();
        }
    }
}
