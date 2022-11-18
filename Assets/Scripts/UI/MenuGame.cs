using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuGame : Menu
{
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

    public void ContinueGame() 
    {
        Player.instance.AddHp(1);
        SetPause(false);
    }

    public void EndGame() 
    {
        CoinCollector.instance.UploadGold();
        SetPause(false);
        LoadScene(0);
    }
}
