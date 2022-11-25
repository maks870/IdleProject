using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text allGoldText;
    [SerializeField] private Text goldMultiplierText;
    [SerializeField] private Button buttonImprove;
    public float goldMultiplier;
    public static Menu instance = null;
    // ������������� �� ������� GetDataEvent � OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // ������������ �� ������� GetDataEvent � OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    protected virtual void Awake() 
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);
    }

    public virtual void SetPause(bool pause) 
    {
    }

    public virtual void EndRound() { }

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

        if (SaveChecker.instance?.activeImprovementsUI == true && buttonImprove != null) 
        {
            buttonImprove.onClick.Invoke();
            SaveChecker.instance.activeImprovementsUI = false;
        }
    }

    // ��� �����, ������� ����� ����������� � ������
    public void GetLoad()
    {
        if (allGoldText != null)
            allGoldText.text = YandexGame.savesData.gold.ToString();
        if (goldMultiplierText != null) 
        {
            int newGold = (int)(YandexGame.savesData.gold * goldMultiplier);
            goldMultiplierText.text = "+" + newGold.ToString();
        }      
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
