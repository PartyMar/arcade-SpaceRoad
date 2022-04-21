using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShip : MonoBehaviour
{
    Rigidbody2D rb;
    private float[] roads = new float[] { -0.445f, -0.31f, -0.175f, -0.04f, 0.095f, 0.23f, 0.365f, 0.5f };
    public int shipPosition = 5;
    private Vector3 currentPosition;
    private Vector3 targetPosition;

    public float duration = 0.4f;
    private float elapsedTime;
    float percentageComplete;
    private bool moving = false;

    [SerializeField]
    private AnimationCurve aCurve;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPosition = new Vector3(roads[shipPosition], -0.2f, -1);
        transform.position = currentPosition;
    }

    void Start()
    {


    }

    void Update()
    {
        if (percentageComplete != 1 && moving == true)
        {
            //Debug.Log(  percentageComplete + "<1");
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / duration;
            transform.position = Vector3.Lerp(currentPosition, targetPosition, aCurve.Evaluate(percentageComplete));
        }
        if (percentageComplete >= 1 && moving == true)
        {
            //Debug.Log(percentageComplete + ">1");
            moving = false;
            currentPosition = targetPosition;
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            moveLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveRight();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MenuButtons.playerIsAlive = false;
    }


    private void moveLeft()
    {
        if (MenuButtons.gamePaused)
        {
            return;
        }
        if (shipPosition != 0 && moving == false)
        {
            moving = true;
            elapsedTime = 0;
            percentageComplete = 0;
            shipPosition--;
            targetPosition = new Vector3(roads[shipPosition], -0.2f, -1);
            //Debug.Log("Moving Left");

        }
    }

    private void moveRight()
    {
        if (MenuButtons.gamePaused)
        {
            return;
        }
        if (shipPosition != 7 && moving == false)
        {
            moving = true;
            elapsedTime = 0;
            percentageComplete = 0;
            shipPosition++;
            targetPosition = new Vector3(roads[shipPosition], -0.2f, -1);
            //Debug.Log("Moving Right");
        }

    }

}

