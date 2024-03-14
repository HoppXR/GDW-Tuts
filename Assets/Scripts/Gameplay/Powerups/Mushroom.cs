using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private bool isMoving = false;
    private float speed = 1.5f;

    void Update()
    {
        if (isMoving)
        {
            transform.position += Vector3.left * (Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMoving = true;
    }
}
