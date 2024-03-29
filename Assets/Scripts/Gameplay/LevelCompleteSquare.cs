using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelCompleteSquare : MonoBehaviour
{
    private Timer _timer;
    
    [SerializeField] private List<Sprite> powerupList;

    private float _changeTimer = 0;
    private int _previousChoice = 5;

    private bool _itemCollected;

    void Start()
    {
        _timer = FindObjectOfType<Timer>();
    }
    
    void Update()
    {
        if (!_itemCollected)
        {
            ChangeImage();
        }
    }

    private void ChangeImage()
    {
        if (_changeTimer < Time.realtimeSinceStartup)
        {
            int choice = (int)Random.Range(0, powerupList.Count - 1);

            if (choice == _previousChoice)
            {
                ChangeImage();
                return;
            }

            GetComponent<SpriteRenderer>().sprite = powerupList[choice];
            
            _changeTimer = Time.realtimeSinceStartup + 0.25f;

            _previousChoice = choice;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<AudioManager>().Stop("Music");
        FindObjectOfType<AudioManager>().Play("LevelClear");
        
        _itemCollected = true;

        StartCoroutine(LevelComplete());
    }

    IEnumerator LevelComplete()
    {
        if (_timer != null)
        {
            _timer.StopTimer();
            int timeLeft = Mathf.RoundToInt(_timer.time);
            int scoreToAdd = timeLeft * 100;
            
            FindObjectOfType<ScoreCounter>().AddScore(scoreToAdd);
        }
        
        yield return new WaitForSeconds(3.252f);
        
        FindObjectOfType<LevelStatus>().SetLevelComplete(true);
    }
}
