using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuGame : Menu
{
    public static MenuGame instance = null;
    private int pauseCount = 0;
    
    public bool IsPaused => pauseCount == 0 ? false : true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
        pauseCount = 0;
        Time.timeScale = 1;
    }

    public void SetPause(bool pause)
    {
        if (pause)
            pauseCount++;
        else 
            pauseCount--;

        if (pauseCount>0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void ContinueGame() 
    {
        Player.instance.AddHp(1);
        SetPause(false);
    }

    public void EndGame(int scene) 
    {
        CoinCollector.instance?.UploadGold();
        LoadScene(scene);
    }
}
