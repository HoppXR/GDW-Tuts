using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    [SerializeField] private float deathTimer = 0.2f;

    private bool isSquashed;
    private bool movingLeft;

    private float flipTimer = 0;
    private float speed = 1.5f;
    
    // Update is called once per frame
    void Update()
    {
        if (!isSquashed)
        {
            Move();
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

    void Move()
    {
        if (movingLeft)
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
        movingLeft = !movingLeft;
    }
}
