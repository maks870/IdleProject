using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveChecker : MonoBehaviour
{
    public static SaveChecker instance = null;
    int oldGold;
    public bool activeImprovementsUI = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetValue();
    }


    private void SetValue()
    {
        oldGold = YandexGame.savesData.gold;
    }

    public void Save()
    {
        if (oldGold != YandexGame.savesData.gold)
        {
            YandexGame.SaveProgress();
            SetValue();
        }           
    }
}
