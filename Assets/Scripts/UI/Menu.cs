using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text allGoldText;
    [SerializeField] private Button button;
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
        if (YandexGame.savesData.isFirstSession)
        {
            YandexGame.savesData.isFirstSession = false;
        }

        if (SaveChecker.instance.activeImprovementsUI && button != null) 
        {
            button.onClick.Invoke();
            SaveChecker.instance.activeImprovementsUI = false;
        }
    }

    // ��� �����, ������� ����� ����������� � ������
    public void GetLoad()
    {
        if (allGoldText != null)
            allGoldText.text = YandexGame.savesData.gold.ToString();
    }

    public void SaveSafely() 
    {
        SaveChecker.instance.Save();
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
