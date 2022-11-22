using System;
using UnityEngine;

public class Stopwatch
{
    private float currentSeconds;
    private bool isActive = false;
    private TimeSpan timeSpan;

    public String GetTime()
    {
        if (isActive)
            currentSeconds += Time.deltaTime;

        timeSpan = TimeSpan.FromSeconds(currentSeconds);
        string time = timeSpan.ToString(@"mm\:ss");
        return time;
    }

    public void StartStopWatch()
    {
        isActive = true;
    }

    public void Reload()
    {
        currentSeconds = 0;
        isActive = true;
    }

    public void Stop()
    {
        isActive = false;
        currentSeconds = 0;
    }

    public void Pause()
    {
        isActive = false;
    }
}
