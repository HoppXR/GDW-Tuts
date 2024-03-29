using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private GameObject scoreDisplay;

    private int score = 0;

    void Update()
    {
        scoreDisplay.GetComponent<NumberDisplayDefinition>()._numericValue = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void RemoveScore(int amount)
    {
        score -= amount;
    }
}
