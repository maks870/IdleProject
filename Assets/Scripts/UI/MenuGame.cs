using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuGame : Menu
{
    [SerializeField] private GameObject endRoundMenu;
    public static MenuGame instance = null;
   
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
    }

    public void SetPause(bool pause)
    {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void EndRound() 
    {
        endRoundMenu.SetActive(true);
    }

    public void EndGame() 
    {
        YandexGame.savesData.gold += Player.instance.Coins;
        YandexGame.SaveProgress();
        LoadScene(0);
    }
}
