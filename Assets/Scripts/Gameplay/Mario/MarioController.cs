using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    [SerializeField] private float runForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;

    [SerializeField] private GameObject bigMarioPrefab;
    [SerializeField] private GameObject smallMarioPrefab;

    private Transform _trans;
    private Rigidbody2D _rb;

    private float _runInput;
    private bool _jumpInput;

    private bool _isGrounded;
    private bool _isBig;
    
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

    void EnemyBounce()
    {
        _rb.AddForce(Vector2.up * jumpForce / 1.5f, ForceMode2D.Impulse);
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
        
        if (collision.gameObject.tag == "Goomba")
        {
            if (collision.contacts[0].normal.y > 0.5)
            {
                EnemyBounce();
                collision.gameObject.GetComponent<Goomba>().SetIsSquashed(true);
            }
            else
            {
                if (!collision.gameObject.GetComponent<Goomba>().GetIsSquashed())
                {
                    if (_isBig)
                    {
                        GetComponent<BoxCollider2D>().size = smallMarioPrefab.GetComponent<BoxCollider2D>().size;
                        _isBig = false;
                    }
                    else
                    {
                        Debug.Log("I Died");
                    }
                }
            }
        }

        if (collision.gameObject.tag == "FlyingGoomba")
        {
            if (collision.contacts[0].normal.y > 0.5)
            {
                EnemyBounce();
                collision.gameObject.GetComponent<FlyingGoomba>().SetIsSquashed(true);
            }
            else
            {
                if (!collision.gameObject.GetComponent<FlyingGoomba>().GetIsSquashed())
                {
                    if (_isBig)
                    {
                        GetComponent<BoxCollider2D>().size = smallMarioPrefab.GetComponent<BoxCollider2D>().size;
                        _isBig = false;
                    }
                    else
                    {
                        Debug.Log("I Died");
                    }
                }
            }
        }
        
        if (collision.gameObject.tag == "Koopa")
        {
            if (collision.contacts[0].normal.y > 0.5 && !collision.gameObject.GetComponent<Koopa>().GetIsKicked())
            {
                EnemyBounce();

                collision.gameObject.GetComponent<Koopa>().SetIsSquashed(true);
                collision.gameObject.GetComponent<Koopa>().SetIsMoving(false);

                collision.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
            }
            else if (collision.gameObject.GetComponent<Koopa>().GetIsSquashed() &&
                     !collision.gameObject.GetComponent<Koopa>().GetIsKicked())
            {
                if (collision.gameObject.transform.position.x > _trans.position.x)
                {
                    collision.gameObject.GetComponent<Koopa>().ApplyKickForce(new Vector2(1, 0));
                }
                if (collision.gameObject.transform.position.x < _trans.position.x)
                {
                    collision.gameObject.GetComponent<Koopa>().ApplyKickForce(new Vector2(1, 0));
                }
            }
            else if (collision.contacts[0].normal.y > 0.5 && collision.gameObject.GetComponent<Koopa>().GetIsKicked())
            {
                EnemyBounce();
                collision.gameObject.GetComponent<Koopa>().SetIsKicked(false);
                collision.gameObject.GetComponent<Koopa>().SetIsMoving(false);

                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                if (collision.gameObject.GetComponent<Koopa>().GetIsMoving())
                {
                    if (_isBig)
                    {
                        GetComponent<BoxCollider2D>().size = smallMarioPrefab.GetComponent<BoxCollider2D>().size;
                        _isBig = false;
                    }
                    else
                    {
                        Debug.Log("I Died");
                    }
                }
            }
        }

        if (collision.gameObject.name.Contains("Mushroom"))
        {
            if (!_isBig)
            {
                Destroy(collision.gameObject);

                _isBig = true;

                GetComponent<BoxCollider2D>().size = bigMarioPrefab.GetComponent<BoxCollider2D>().size;
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
        
        if (collision.gameObject.name.Contains("FireFlower"))
        {
            Destroy(collision.gameObject);

            _isBig = true;

            GetComponent<BoxCollider2D>().size = bigMarioPrefab.GetComponent<BoxCollider2D>().size;
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}

