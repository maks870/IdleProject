using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

public class MenuGame : Menu
{
    [SerializeField] private Text stopwatchGame;
    [SerializeField] private Text stopwatchMenu;
    [SerializeField] private GameObject newRecordUI;
    [SerializeField] private GameObject tipsPanel;
    [SerializeField] private GameObject rewardedButton;
    [SerializeField] private LeaderboardYG leaderboardYG;
    private List<GameObject> tips = new List<GameObject>();
    private Stopwatch stopwatch;
    private int pauseCount = 0;
    public UnityEvent endRoundEvent; 
    

    public bool IsPaused => pauseCount == 0 ? false : true;

    protected override void Awake()
    {
        Time.timeScale = 0;
        base.Awake();
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
    }

    public override void SetPause(bool pause)
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

    private void ResetPause()
    {
        pauseCount = 0;
        Time.timeScale = 1;
    }

    public override void EndRound() 
    {
        base.EndRound();
        if (CoinCollector.instance.CollectedGold > 1)
            rewardedButton.gameObject.SetActive(true);
        else
            rewardedButton.gameObject.SetActive(false);

        if (YandexGame.savesData.recordScore < CoinCollector.instance.CollectedGold)//Сохранение нового рекорда
        {
            newRecordUI.gameObject.SetActive(true);
            YandexGame.savesData.recordScore = CoinCollector.instance.CollectedGold;
            YandexGame.NewLeaderboardScores("Leaderboard", CoinCollector.instance.CollectedGold);
            leaderboardYG?.UpdateLB();
        }

        SetPause(true);
        ActivateTips();
        endRoundEvent.Invoke();
    }

    public void EndGame(int scene)
    {
        YandexGame.savesData.gold += CoinCollector.instance.CollectedGold;
        SaveChecker.instance.Save();
        ResetPause();
        LoadScene(scene);
    }

    public void ActivateTips()
    {
        int r = Random.Range(0, tips.Count);
        tips[r].SetActive(true);
    }

    public void ActiveImprovementsUI()
    {
        SaveChecker.instance.activeImprovementsUI = true;
        EndGame(0);
    }
}