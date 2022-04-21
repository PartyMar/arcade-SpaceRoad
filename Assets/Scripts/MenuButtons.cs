using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour
{
    public static bool gamePaused = false;
    public static bool gameStarted = false;
    public static bool playerIsAlive = false;

    //HUD
    [SerializeField]
    private GameObject pauseCanvas;
    [SerializeField]
    private GameObject hudCanvas;
    [SerializeField]
    private GameObject mainMenuCanvas;
    [SerializeField]
    private GameObject resultCanvas;

    //selected Menu
    [SerializeField]
    private GameObject selectedStartB;
    [SerializeField]
    private GameObject selectedPauseB;
    [SerializeField]
    private GameObject selectedScoreB;

    //Player
    [SerializeField]
    private GameObject blueShip;
    private GameObject blueShipInScene = null;


    //ScoreSystem
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text scoreTextResult;

    private float score;
    private int multiplier = 1;
    private float time = 0;

    void Start()
    {

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (playerIsAlive)
            {
                Pause();
            }
        }


        if (gameStarted == true && blueShipInScene == null)
        {
            setPlayerIsAlive(false);

            scoreTextResult.text = "" + (int)score;
            resultCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(selectedScoreB);
        }
    }

    private void FixedUpdate()
    {
        if (playerIsAlive)
        {
            time += Time.deltaTime;
            addScore((Time.deltaTime));
            if (time > 3)
            {
                time = 0;
                multiplier += 1;
                Time.timeScale = 1f + (0.04f * multiplier);
            }
        }

    }



    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        score = 0;
        multiplier = 1;
        time = 0;
        hudCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);

        setPlayerIsAlive(true);

    }

    public void MainMenu()
    {
        destroyAllShips();
        setPlayerIsAlive(false);
        resultCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedStartB);
    }

    public void Pause()
    {
        if (gamePaused)
        {
            pauseCanvas.SetActive(false);
            Time.timeScale = 1f + (0.025f * multiplier);
            gamePaused = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            pauseCanvas.SetActive(true);
            Time.timeScale = 0f;
            gamePaused = true;
            EventSystem.current.SetSelectedGameObject(selectedPauseB);
        }
    }




    private void setPlayerIsAlive(bool bl)
    {
        playerIsAlive = bl;

        if (bl == true)
        {
            blueShipInScene = Instantiate(blueShip);
            gameStarted = true;
        }
        else
        {
            gameStarted = false;
            gamePaused = false;
            pauseCanvas.SetActive(false);
            hudCanvas.SetActive(false);
            Time.timeScale = 1f;
            EventSystem.current.SetSelectedGameObject(null);

            if (blueShipInScene != null)
            {
                Destroy(blueShipInScene);
            }
        }
    }

    private void destroyAllShips()
    {
        GameObject[] ships;
        ships = GameObject.FindGameObjectsWithTag("Other");
        foreach (GameObject sp in ships)
        {
            Destroy(sp);
        }
    }

    void addScore(float scoreAdded)
    {
        score += scoreAdded * multiplier;
        scoreText.text = "  S C O R E :  " + (int)score;
    }
}
