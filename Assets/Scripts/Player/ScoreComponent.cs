using System;
using UnityEngine;

public class ScoreComponent : MonoBehaviour
{
    public int CoinAmount { get; private set; } = 0;

    public event Action<int> MoneyChangedEvent;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            EarnCoin();

            Destroy(collision.gameObject);
        }
    }

    void EarnCoin()
    {
        CoinAmount++;
        MoneyChangedEvent(CoinAmount);
    }
}
