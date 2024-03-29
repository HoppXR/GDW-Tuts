using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialLives : MonoBehaviour
{
    [SerializeField] private int initialLives = 5;
    
    void Start()
    {
        PlayerPrefs.SetInt("Lives", initialLives);
    }

}
