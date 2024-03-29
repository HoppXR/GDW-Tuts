using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private GameObject coinDisplay;

    private int coinCount = 0;

    void Update()
    {
        coinDisplay.GetComponent<NumberDisplayDefinition>()._numericValue = coinCount.ToString();
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    public void AddCoin(int amount)
    {
        coinCount += amount;
    }

    public void RemoveCoin(int amount)
    {
        coinCount -= amount;
    }
}
