using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class RewardedAd : MonoBehaviour
{
	[SerializeField] private Menu menu;
	// ������������� �� ������� �������� ������� � OnEnable
	private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

	// ������������ �� ������� �������� ������� � OnDisable
	private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

	// ����������� ����� ��������� �������
	void Rewarded(int id)
	{
		if (id == 1) AddGold(300);

		else if (id == 2) MultyplyGold();
	}

	// ����� ��� ������ ����� �������
	public void ExampleOpenRewardAd(int id)
	{
		// �������� ����� �������� ����� �������
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
