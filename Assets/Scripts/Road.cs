using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Road : MonoBehaviour
{

    private float[] roads = new float[8] { -0.445f, -0.31f, -0.175f, -0.04f, 0.095f, 0.23f, 0.365f, 0.5f };


    [SerializeField]
    private float timeIntervalMed;
    private bool medIsRight = false;


    [SerializeField]
    private float timeIntervalRight;
    private int trafficRight = 2;
    [SerializeField]
    private float timeIntervalLeft;
    private int trafficLeft = 1;


    private float countToSwitch = 0;
    private float timerToSwitch = 15;


    private float countToDrunkTruck = 0;
    private float timerToDrunkTruck = 45;
    private bool drunkTime = false;


    [SerializeField]
    private GameObject[] ships;



    void Start()
    {
        InvokeRepeating("createShipRight", 1, timeIntervalRight);
        InvokeRepeating("createShipLineLeft", 1, timeIntervalLeft);
        InvokeRepeating("createMedShip", 1, timeIntervalMed);
    }

    private void FixedUpdate()
    {
        if (MenuButtons.playerIsAlive)
        {
            countToSwitch += Time.deltaTime;
            if (countToSwitch >= timerToSwitch)
            {
                countToSwitch = 0;
                timerToSwitch = Random.Range(10, 15);
                int traffic = (int)Random.Range(1, 4);
                trafficRight = traffic;
                trafficLeft = 3 - traffic;
            }

            calculateDrunkChanse();
        }
    }


    void createShipRight()
    {
        if (MenuButtons.playerIsAlive)
        {
            List<float> leftRoads = new List<float>() { roads[4], roads[5], roads[6] };

            for (int i = 0; i < trafficRight; i++)
            {
                int position = (int)Random.Range(0, leftRoads.Count);

                int shipNumber = (int)Random.Range(0, 2);
                GameObject ship = Instantiate(ships[(int)Random.Range(0, 2)], new Vector3(leftRoads[position], 0.45f, -1), Quaternion.identity);
                var shipPar = ship.GetComponent<OtherShip>();
                shipPar.setRoadSide("Right");
                if (this.drunkTime && shipNumber == 1)
                {
                    var nums = new float[2] { -0.3f, 0.3f };
                    int randNumber = (int)Random.Range(0, 2);
                    shipPar.setVectorOfVelocity(-1f, nums[randNumber]);
                    ship.GetComponent<HealthSystem>().setArmor(4);

                    this.drunkTime = false;
                    Debug.Log("DrunkTime!");
                }

                leftRoads.RemoveAt(position);
            }
        }


    }

    void createShipLineLeft()
    {
        if (MenuButtons.playerIsAlive)
        {
            List<float> rightRoads = new List<float>() { roads[1], roads[2], roads[3] };

            for (int i = 0; i < trafficLeft; i++)
            {
                int position = (int)Random.Range(0, rightRoads.Count);

                int shipNumber = (int)Random.Range(0, 2);
                GameObject ship = Instantiate(ships[shipNumber], new Vector3(rightRoads[position], 0.45f, -1), Quaternion.Euler(180, 0, 0));

                var shipPar = ship.GetComponent<OtherShip>();
                shipPar.setRoadSide("Left");

                rightRoads.RemoveAt(position);
                if (this.drunkTime && shipNumber == 1)
                {
                    var numRandom = Random.Range(0, 20);
                    if (numRandom <= 15)
                    {
                        return;
                    }
                    var nums = new float[2] { -0.3f, 0.3f };
                    int randNumber = (int)Random.Range(0, 2);
                    shipPar.setVectorOfVelocity(-1f, nums[randNumber]);
                    ship.GetComponent<HealthSystem>().setArmor(4);

                    this.drunkTime = false;
                    Debug.Log("DrunkTime!");
                }


            }
        }
    }

    void createMedShip()
    {
        if (medIsRight)
        {
            medIsRight = false;
            GameObject ship = Instantiate(ships[2], new Vector3(roads[7], -0.7f, -1), Quaternion.identity);
            var shipStuff = ship.GetComponent<MedShip>();
            shipStuff.setRoadSide("Right");
            shipStuff.setVectorOfVelocity(1f);
        }
        else
        {
            medIsRight = true;
            GameObject ship = Instantiate(ships[2], new Vector3(roads[0], 0.7f, -1), Quaternion.Euler(180, 0, 0));
            var shipStuff = ship.GetComponent<MedShip>();
            shipStuff.setRoadSide("Left");

        }
        timeIntervalMed = (float)Random.Range(10, 16);
    }



    void calculateDrunkChanse()
    {
        if (!drunkTime)
        {
            countToDrunkTruck += Time.deltaTime;
        }
        if (countToDrunkTruck >= timerToDrunkTruck)
        {
            countToDrunkTruck = 0;
            timerToDrunkTruck = Random.Range(30, 61);
            drunkTime = true;
            Debug.Log("GetReadyToDrunkGuy! " + countToDrunkTruck);
        }
    }

}

