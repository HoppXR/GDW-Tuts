using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    [SerializeField] private float runForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;

    private Transform _trans;
    private Rigidbody2D _rb;

    private float _runInput;
    private bool _jumpInput;

    private bool _isGrounded;
    
    private void Start()
    {
        _trans = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _runInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W))
        {
            _jumpInput = true;
        }
        else
        {
            _jumpInput = false;
        }

        if (_runInput == 0 && _rb.velocity.y == 0)
        {
            _rb.drag = 3;
        }
        else
        {
            _rb.drag = 1;
        }
    }

    private void FixedUpdate()
    {
        if (_runInput != 0)
        {
            Run();
        }

        if (_jumpInput && _isGrounded)
        {
            Jump();
        }
    }

    private void Run()
    {
        if (Mathf.Abs(_rb.velocity.x) >= maxSpeed)
        {
            return;
        }

        if (_runInput > 0)
        {
            _rb.AddForce(Vector2.right * runForce, ForceMode2D.Force);
            _trans.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (_runInput < 0)
        {
            _rb.AddForce(Vector2.left * runForce, ForceMode2D.Force);
            _trans.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y > 0.5)
            {
                _isGrounded = true;
            }
        }
    }
}
