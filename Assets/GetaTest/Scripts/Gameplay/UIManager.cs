using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Timers")] public float raceTime;
    public float timeToFade;
    public float timeCountDown;
    private IEnumerator countDown;
    private bool startGame;
    public string onLose;
    public string onwin;

    [Header("UI")] public TextMeshProUGUI txt_countDown;
    public TextMeshProUGUI txt_playerTIme;
    public TextMeshProUGUI txt_screenMs;
    public TextMeshProUGUI txt_lapCounter;
    public TextMeshProUGUI txt_maxLaps;
    public Image panelMenu;
    public float speedMenu;
    private IEnumerator showHideMenu;
    
    public delegate void launchGame();

    public event launchGame onLaunchGame;

    public delegate void timeEnd();

    public event timeEnd onTimeEnd;

    private void Start()
    {
        FindObjectOfType<NotifyTime>().onGetTime += AddTime;
        FindObjectOfType<CheckPointCounter>().onActualizeLap += ActualizeLapCount;
        FindObjectOfType<GameController>().onRaceRules += ActualizeMaxLaps;
        FindObjectOfType<GameController>().onWinGame += EndGame;
        //Fade
        StartCountDown();
    }

    private void Update()
    {
        if (startGame)
        {
            PlayerTime();
        }
    }

    void ActualizeLapCount(int actualLap)
    {
        txt_lapCounter.text = actualLap.ToString();
    }

    void ActualizeMaxLaps(int maxLaps)
    {
        txt_maxLaps.text = $"/ " + maxLaps;
    }

    void StartCountDown()
    {
        if (countDown != null)
        {
            StopCoroutine(countDown);
        }

        countDown = CountDown();
        StartCoroutine(countDown);
    }

    IEnumerator CountDown()
    {
        txt_countDown.gameObject.SetActive(true);
        while (timeCountDown > 0)
        {
            yield return null;
            SetUiCountDown((int) timeCountDown);
            timeCountDown -= Time.deltaTime;
        }

        txt_countDown.gameObject.SetActive(false);
        txt_lapCounter.gameObject.SetActive(true);
        txt_maxLaps.gameObject.SetActive(true);
        txt_playerTIme.gameObject.SetActive(true);
        onLaunchGame();
        startGame = true;
    }

    private void SetUiCountDown(int value)
    {
        txt_countDown.text = value != 0 ? value.ToString() : "GO!!!";
    }

    void PlayerTime()
    {
        if (!startGame)
        {
            return;
        }

        raceTime -= Time.deltaTime;
        txt_playerTIme.text = String.Format("{0:.00}", raceTime);
        if (raceTime <= 0)
        {
            raceTime = 0;
            startGame = false;
            onTimeEnd();
            TurnOffUI();
            EndGame(false);
        }
    }

    void AddTime(float timeToAdd)
    {
        raceTime += timeToAdd;
    }

    void EndGame(bool winGame)
    {
        txt_screenMs.text = winGame ? onwin : onLose;
        startGame = false;
        TurnOffUI();
        StartShowHideMenu(0);
    }

    void TurnOffUI()
    {
        txt_lapCounter.gameObject.SetActive(false);
        txt_maxLaps.gameObject.SetActive(false);
        txt_playerTIme.gameObject.SetActive(false);
        txt_screenMs.gameObject.SetActive(true);
    }

    public void StartShowHideMenu(float target_Y)
    {
        if (showHideMenu != null)
        {
            StopCoroutine(showHideMenu);
        }

        showHideMenu = ShowHideMenu(target_Y);
        StartCoroutine(showHideMenu);
    }

    IEnumerator ShowHideMenu(float target_y)
    {
        panelMenu.gameObject.SetActive(true);
        while (panelMenu.transform.position.y != target_y)
        {
            yield return null;
            float desirePosY = Mathf.MoveTowards(panelMenu.transform.position.y, target_y, Time.deltaTime * speedMenu);
            panelMenu.transform.position = new Vector3(panelMenu.transform.position.x, desirePosY, 0);
        }
    }
}