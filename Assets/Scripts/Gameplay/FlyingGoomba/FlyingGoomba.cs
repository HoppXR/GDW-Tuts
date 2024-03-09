using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGoomba : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    [SerializeField] private float deathTimer = 0.2f;

    private bool isSquashed;
    private bool movingLeft;

    private float flipTimer = 0;
    private float speed = 1.5f;
    private float jumpForce = 5;

    private bool _isGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSquashed)
        {
            Move();

            if (_isGrounded)
            {
                Jump();
            }
        }

        if (isSquashed)
        {
            Destroy(gameObject, deathTimer);
        }

        if (flipTimer <= Time.realtimeSinceStartup)
        {
            transform.Rotate(new Vector3(0, 1, 0), 180);
            flipTimer = Time.realtimeSinceStartup + 0.25f;
        }
    }
    
    private void Jump()
    {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _isGrounded = false;
    }

    void Move()
    {
        if (!movingLeft)
        {
            transform.position += Vector3.left * (Time.deltaTime * speed);
        }
        else
        {
            transform.position += Vector3.right * (Time.deltaTime * speed);
        }
    }

    public bool GetIsSquashed()
    {
        return isSquashed;
    }

    public void SetIsSquashed(bool squashed)
    {
        isSquashed = squashed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isGrounded)
        {
            movingLeft = !movingLeft;
        }
        
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y > 0.5)
            {
                _isGrounded = true;
            }
        }
    }
}
