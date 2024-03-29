using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<CoinCounter>().AddCoin(1);
        FindObjectOfType<ScoreCounter>().AddScore(100);
        
        FindObjectOfType<AudioManager>().Play("Coin");
        Destroy(gameObject);
    }
}
