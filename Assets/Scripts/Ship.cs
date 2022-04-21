using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    public float speedLeft = 0;
    [SerializeField]
    public float speedRight = 0;

    [HideInInspector]
    public float speed = 0;

    private float vectorOfVelocityY = -1f;
    private float vectorOfVelocityX = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(this.vectorOfVelocityX * speed, this.vectorOfVelocityY * speed);

    }
    public void setRoadSide(string road)
    {
        switch (road)
        {
            case "Left":
                {
                    speed = speedLeft;
                    break;
                }
            case "Right":
                {
                    speed = speedRight;
                    break;
                }
        }
    }

    public void setSpeed(float closeShipSpeed)
    {
        this.speed = closeShipSpeed;
    }

    public void setVectorOfVelocity(float velY, float velX = 0)
    {
        this.vectorOfVelocityY = velY;
        this.vectorOfVelocityX = velX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var colHP = collision.gameObject.GetComponent<HealthSystem>();
        var ownHP = this.gameObject.GetComponent<HealthSystem>();
        this.vectorOfVelocityY -= 1;
        ownHP.damageBoth(colHP, ownHP);
        
    }
}
