using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button soundOn;
    [SerializeField] private Button soundOff;
    // ������������� �� ������� GetDataEvent � OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // ������������ �� ������� GetDataEvent � OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Start()
    {
        // ��������� ���������� �� ������
        if (YandexGame.SDKEnabled == true)
        {
            // ���� ����������, �� ��������� ��� �����
            GetLoad();

            // ���� ������ ��� �� �����������, �� ����� �� ����������� � ������ Start,
            // �� �� ���������� ��� ������ ������� GetDataEvent, ����� ��������� �������
        }
    }

    // ��� �����, ������� ����� ����������� � ������
    public void GetLoad()
    {
        if (YandexGame.savesData.isFirstSession)
        {
#if !UNITY_EDITOR
			YandexGame.SwitchLanguage(YandexGame.EnvironmentData.language);
#else
            YandexGame.SwitchLanguage("en");
#endif
            YandexGame.savesData.isFirstSession = false;
            YandexGame.SaveProgress();
        }
        else
        {
            YandexGame.SwitchLanguage(YandexGame.savesData.language);
        }

        SwapButtonSound();
    }
    public void SetSound(bool enable)
    {
        YandexGame.savesData.sound = enable;
        YandexGame.SaveProgress();
        SwapButtonSound();
    }

    private void SwapButtonSound()
    {
        soundOff.gameObject.SetActive(!YandexGame.savesData.sound);
        soundOn.gameObject.SetActive(YandexGame.savesData.sound);
    }
}
