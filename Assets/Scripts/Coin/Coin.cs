using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IAward
{
    [SerializeField] private int coinValue;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Coin()
    { 
    
    }
    public int GetValue => coinValue;
    public SpriteRenderer GetSpriteRenderer => spriteRenderer;

    public Sprite GetAwardSprite => spriteRenderer.sprite;

    public string GetAwardName => coinValue + " Coins";

    public string GetAwardDescription => "Simple coins";

    public bool GetAwardAccessibility => true;

    public void AwardAction()
    {
        CoinCollector.instance.AddCoin(coinValue);
    }
    public void ChangeCoin(int coinValue, Sprite sprite)
    {
        this.coinValue = coinValue;
        spriteRenderer.sprite = sprite;
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
