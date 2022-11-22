using UnityEngine;
using UnityEngine.UI;


public class MenuGame : Menu
{
    [SerializeField] private Text stopwatchText;
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

        pauseCount = 0;
        Time.timeScale = 1;

        stopwatch = new Stopwatch();
        stopwatch.StartStopWatch();
    }

    private void Update()
    {
        stopwatchText.text = stopwatch.GetTime();
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
        stopwatch.Stop();
        CoinCollector.instance?.UploadGold();
        LoadScene(scene);
    }
}
