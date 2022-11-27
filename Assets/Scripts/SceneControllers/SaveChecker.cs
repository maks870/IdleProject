using UnityEngine;
using YG;

public class SaveChecker : MonoBehaviour
{
    private int oldGold;
    public static SaveChecker instance = null;
    public bool activeImprovementsUI = false;

    private void OnEnable() => YandexGame.GetDataEvent += SetValue;

    private void OnDisable() => YandexGame.GetDataEvent -= SetValue;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            SetValue();
        }
    }

    private void SetValue()
    {
        oldGold = YandexGame.savesData.gold;
    }

    public void Save()
    {
        if (oldGold != YandexGame.savesData.gold)
        {
            YandexGame.SaveProgress();
            SetValue();
        }
    }
}
