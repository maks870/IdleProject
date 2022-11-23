using UnityEngine;

public class Coin : Award
{
    [SerializeField] private int coinValue;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int GetValue => coinValue;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public SpriteRenderer GetSpriteRenderer => spriteRenderer;

    public override void AwardAction()
    {
        CoinCollector.instance.AddCoin(coinValue);
    }

    public void ChangeCoin(int coinValue, Sprite sprite)
    {
        this.coinValue = coinValue;
        spriteRenderer.sprite = sprite;
    }
}
