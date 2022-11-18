using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button soundOn;
    [SerializeField] private Button soundOff;
    [SerializeField] private Text goldText;
    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        // Проверяем запустился ли плагин
        if (YandexGame.SDKEnabled == true)
        {
            // Если запустился, то запускаем Ваш метод
            GetLoad();

            // Если плагин еще не прогрузился, то метод не запуститься в методе Start,
            // но он запустится при вызове события GetDataEvent, после прогрузки плагина
        }
    }

    // Ваш метод, который будет запускаться в старте
    public void GetLoad()
    {
        goldText.text = YandexGame.savesData.gold.ToString();
        SwapButtonSound(YandexGame.savesData.sound);
    }
    public void SetSound(bool enable)
    {
        YandexGame.savesData.sound = enable;
        YandexGame.SaveProgress();
        SwapButtonSound(enable);
    }

    private void SwapButtonSound(bool sound)
    {
        soundOff.gameObject.SetActive(!sound);
        soundOn.gameObject.SetActive(sound);
    }
}
