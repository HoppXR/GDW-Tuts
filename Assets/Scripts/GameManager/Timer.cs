using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject timerDisplay;

    public float time = 300;
    private float _timePassed = 0f;
    public bool timerActive;

    private void Start()
    {
        timerActive = true;
    }

    void Update()
    {
        if (!timerActive) return;
        
        _timePassed += Time.deltaTime;
        time = Mathf.RoundToInt(300 - _timePassed);
        
        timerDisplay.GetComponent<NumberDisplayDefinition>()._numericValue = time.ToString();

        if (time == 0)
        {
            FindObjectOfType<LevelStatus>().SetLevelFailed(true);
        }
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
