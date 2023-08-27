using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Ship and Station setting")] 
    public GameObject shipPrefab;
    public Transform shipStartPoint;
    public GameObject currentShip {get; private set;}
    
    public GameObject stationPrefab;
    public Transform stationStartPoint;
    public GameObject currentStation {get; private set;}    

    [Header("UI")]
    public GameObject inGameUI;
    public GameObject pausedUI;
    public GameObject mainMenuUI;
    public GameObject gameOverUI;
    //Предупреждающая рамка
    public GameObject warningUI;
    public Text textScore;
    public Text textHighScore;
    public Text textStationHits;

    [Header("Other setting")]
    //Сценарий управления камерой
    public SmoothFollow cameraFollow;
    //Проигрывается игра?    
    public bool gameIsPlaying {get; private set;}

    //Система создания астеройдов
    public AsteroidSpawner asteroidSpawner;
    
    //Система управления индикаторами
    public IndicatorManager indicatorManager;

    //Границы игры
    public Boundary boundary;    

    public bool paused;

    
    private DamageTaking stationDT;
   
    private int score = 0;


   

    void ShouUI(GameObject newUI)
    {
        GameObject[] allUI = {inGameUI, pausedUI, gameOverUI, mainMenuUI};
        //Скрыть все UI
        foreach(GameObject UIToHide in allUI)
        {
            UIToHide.SetActive(false);
        }
        //отобразить указанный
        newUI.SetActive(true);
    }

    public void ShowMainMenu()
    {
        ShouUI(mainMenuUI);

        gameIsPlaying = false;
        asteroidSpawner.spawnAsteroids = false;
    }

    public void StartGame()
    {
        ShouUI(inGameUI);
        gameIsPlaying = true;

        score = 0;
        textScore.text = ($"{score}");

        if (currentShip != null)
        {
            Destroy(currentShip);
        }
        if(currentStation != null)
        {
            Destroy(currentStation);
        }
                
        currentShip = Instantiate(shipPrefab, shipStartPoint.position, shipStartPoint.rotation);      
        
        currentStation = Instantiate(stationPrefab, shipStartPoint.position, shipStartPoint.rotation);
        stationDT = currentStation.GetComponent<DamageTaking>();

        cameraFollow.target = currentShip.transform;

        indicatorManager.objectDistance = currentShip;

        asteroidSpawner.spawnAsteroids = true;

        asteroidSpawner.target = currentStation.transform;
        
    }
    
    public void GameOver()
    {
        ShouUI(gameOverUI);
        textHighScore.text = ($"High score: {score}");
        gameIsPlaying = false;

         if(currentShip != null)
        {
            Destroy(currentShip);
        }
        if(currentStation != null)
        {
            Destroy(currentStation);
        }
         asteroidSpawner.spawnAsteroids = false;
         asteroidSpawner.DestroyAllAsteroids();

         //Скрыть рамку
         warningUI.SetActive(false);

    }

    public void SetPaused(bool paused)
    {
        inGameUI.SetActive(!paused);
        pausedUI.SetActive(paused);
        // Если игра приостановлена...
        if (paused)
        {
            // Остановить время
            Time.timeScale = 0.0f;
        } 
        else 
        {
            // Возобновить ход времени
            Time.timeScale = 1.0f;
        }
    }

    public void AddScore(int value)
    {
        score += value;
        textScore.text = ($"{score}");
    }

    private void Start()
    {
        ShowMainMenu();
    }

    private void Update()
    {
        if( currentShip == null )
            return;

        float distance = (currentShip.transform.position - boundary.transform.position).magnitude;

        if( distance > boundary.destroyRadius )
        {
            GameOver();
        }
        else if( distance > boundary.warningRadius )
        {
            warningUI.SetActive(true);
        }
        else
        {
            warningUI.SetActive(false);
        }

        
        textStationHits.text = ($" Station  hits: {stationDT.GetHitsPoints()}");
        
    }
}
