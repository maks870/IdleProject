using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class MenuGame : Menu
{
    [SerializeField] private Text stopwatchGame;
    [SerializeField] private Text stopwatchMenu;
    [SerializeField] private GameObject newRecordUI;
    [SerializeField] private GameObject tipsPanel;
    private List<GameObject> tips = new List<GameObject>();
    private Stopwatch stopwatch;
    private int pauseCount = 0;
    public static MenuGame instance = null;

    public bool IsPaused => pauseCount == 0 ? false : true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        ResetPause();

        stopwatch = new Stopwatch();
        stopwatch.StartStopWatch();

        foreach (Transform child in tipsPanel.transform)
            tips.Add(child.gameObject);
    }

    private void Update()
    {
        string timeString = stopwatch.GetTime();//вызывать только один раз за кадр, не чаще
        //или создать отдельный объект 

        stopwatchGame.text = timeString;
        stopwatchMenu.text = timeString;

        if (YandexGame.savesData.recordScore < CoinCollector.instance.CollectedGold)
            newRecordUI.gameObject.SetActive(true);
    }

    public void SetPause(bool pause)
    {
        if (pause)
            pauseCount++;
        else
            pauseCount--;

        if (pauseCount > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void ContinueGame()
    {
        Player.instance.AddHp(1);
        SetPause(false);
    }

    private void ResetPause()
    {
        Time.timeScale = 1;
        pauseCount = 0;
    }

    public void EndGame(int scene)
    {
        YandexGame.savesData.gold += CoinCollector.instance.CollectedGold;

        if (YandexGame.savesData.recordScore < CoinCollector.instance.CollectedGold)
        {
            YandexGame.savesData.recordScore = CoinCollector.instance.CollectedGold;
            YandexGame.NewLeaderboardScores("Leaderboard", CoinCollector.instance.CollectedGold);
        }

        ResetPause();
        YandexGame.SaveProgress();
        LoadScene(scene);
    }

    public void ActivateTips()
    {
        int r = Random.Range(0, tips.Count);
        Debug.Log(r);
        tips[r].SetActive(true);
    }

    public void ActiveImprovementsUI()
    {
        SaveChecker.instance.activeImprovementsUI = true;
        LoadScene(0);
    }
}