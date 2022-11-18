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
