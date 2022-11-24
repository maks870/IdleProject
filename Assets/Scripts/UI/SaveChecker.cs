using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveChecker : MonoBehaviour
{
    int oldGold;

    public void SetValue()
    {
        oldGold = YandexGame.savesData.gold;
    }

    public void Save()
    {
        if (oldGold != YandexGame.savesData.gold)
            YandexGame.SaveProgress();
    }
}
