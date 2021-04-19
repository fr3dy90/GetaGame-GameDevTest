using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStatus : MonoBehaviour
{
    public static RaceStatus Instance;
    private GameController controller;
    private int maxLaps;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        controller = GetComponent<GameController>();
        maxLaps = controller.maxLaps;
    }

    public void GetActualLap(int actualLap)
    {
        if (actualLap > maxLaps)
        {
            controller.OnWinGame();
        }
    }
}
