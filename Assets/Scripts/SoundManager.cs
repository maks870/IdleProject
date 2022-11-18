using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public UnityEvent soundOn;
    [SerializeField] public UnityEvent soundOff;
    public static SoundManager instance;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
       // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }

    private void GetLoad() 
    {
        bool enable = YandexGame.savesData.sound;
        if (enable)
        {
            soundOn.Invoke();
        }
        else
        {
            soundOff.Invoke();
        }
    }

    public void SetSound(bool enable) 
    {
        YandexGame.savesData.sound = enable;
        YandexGame.SaveProgress();

        if (enable) soundOn.Invoke();
        else soundOff.Invoke();
    }
}
