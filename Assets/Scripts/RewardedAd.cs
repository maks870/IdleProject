using UnityEngine;
using UnityEngine.UI;
using YG;

public class RewardedAd : MonoBehaviour
{
    private Menu menu;
    // Подписываемся на событие открытия рекламы в OnEnable
    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    private void Start()
    {
        menu = Menu.instance;
    }

    // Подписанный метод получения награды
    void Rewarded(int id)
    {
        switch (id)
        {
            case 1:
                AddGold(300);
                break;
            case 2:
                MultyplyCollectorGold(1f);
                break;
            case 3:
            default:
                MultyplyAllGold(menu.goldMultiplier);
                break;
        }
    }

    // Метод для вызова видео рекламы
    public void ExampleOpenRewardAd(int id)
    {
        // Вызываем метод открытия видео рекламы
        YandexGame.RewVideoShow(id);
    }

    private void AddGold(int gold)
    {
        YandexGame.savesData.gold += gold;
        YandexGame.SaveProgress();
        menu?.GetLoad();
    }


    private void MultyplyAllGold(float multiply)
    {
        float newGold = YandexGame.savesData.gold * multiply;
        if (newGold < 10)
            newGold = 10;
        AddGold((int)newGold);
    }

    private void MultyplyCollectorGold(float multiply)
    {
        if (CoinCollector.instance == null)
            return;

        float newGold = CoinCollector.instance.CollectedGold * multiply;
        CoinCollector.instance.AddCoin((int)newGold);
    }
}
