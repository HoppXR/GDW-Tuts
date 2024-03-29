using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelCompleteSquare : MonoBehaviour
{
    [SerializeField] private List<Sprite> powerupList;

    private float _changeTimer = 0;
    private int _previousChoice = 5;

    private bool _itemCollected;
    
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
        FindObjectOfType<LevelStatus>().SetLevelComplete(true);
        
        FindObjectOfType<AudioManager>().Stop("Music");
        FindObjectOfType<AudioManager>().Play("LevelClear");
        
        _itemCollected = true;
    }
}
