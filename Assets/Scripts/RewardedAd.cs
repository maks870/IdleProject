using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class RewardedAd : MonoBehaviour
{
	[SerializeField] private Menu menu;
	// Подписываемся на событие открытия рекламы в OnEnable
	private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

	// Отписываемся от события открытия рекламы в OnDisable
	private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

	// Подписанный метод получения награды
	void Rewarded(int id)
	{
		if (id == 1) AddGold(300);

		else if (id == 2) MultyplyGold();
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

	private void MultyplyGold() 
	{
		if (CoinCollector.instance != null) 
		{
			CoinCollector.instance.AddCoin(CoinCollector.instance.CollectedGold);
		}
	}
}
