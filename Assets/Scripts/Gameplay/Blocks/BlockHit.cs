using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    [SerializeField] private GameObject blockItem;
    
    [SerializeField] private Sprite usedBlock;
    [SerializeField] private Sprite unusedBlock;
    private SpriteRenderer _spriteRenderer;

    private bool blockHit = false;
    private bool blockHitActionPerformed = false;

    private GameObject item = null;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!blockHit)
        {
            _spriteRenderer.sprite = unusedBlock;
        }
        else
        {
            _spriteRenderer.sprite = usedBlock;

            if (!blockHitActionPerformed)
            {
                BlockHitAction();
            }
        }
    }

    void BlockHitAction()
    {
        if (blockItem.CompareTag("Coin"))
        {
            item = Instantiate(blockItem, transform.position + new Vector3(0, 1, 0), Quaternion.Euler(0, 0, 0));
            Destroy(item, 0.5f);
            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            
            FindObjectOfType<CoinCounter>().AddCoin(1);
            FindObjectOfType<ScoreCounter>().AddScore(100);
            
            FindObjectOfType<AudioManager>().Play("Coin");
        }
        else if (blockItem.CompareTag("Powerup"))
        {
            item = Instantiate(blockItem, transform.position + new Vector3(0, 1.01f, 0), Quaternion.Euler(0, 0, 0));
            item.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            
            FindObjectOfType<AudioManager>().Play("MushroomSpawn");
        }

        blockHitActionPerformed = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.contacts[0].normal.y > 0.5)
            {
                blockHit = true;
                
                FindObjectOfType<AudioManager>().Play("Bump");
            }
        }
    }
}
