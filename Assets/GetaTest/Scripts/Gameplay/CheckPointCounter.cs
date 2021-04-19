using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCounter : MonoBehaviour
{
    private int lap = 1;
    private int checkPoint = 0;
    private int chepointCount;
    private int nextCheckpoint = 1;
    private bool doOnce;
    
    public delegate void actualizeLap(int lap);

    public event actualizeLap onActualizeLap;

    private void Start()
    {
        chepointCount = GameObject.FindGameObjectsWithTag("CheckPoint").Length;
        doOnce = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint")
        {
            if (doOnce)
            {
                doOnce = false;
                return;
            }

            int n = int.Parse(other.name);
            if (n == nextCheckpoint)
            {
                checkPoint = n;
                if (checkPoint == 0)
                {
                    lap++;
                    onActualizeLap(lap);
                    RaceStatus.Instance.GetActualLap(lap);
                }

                nextCheckpoint++;
                if (nextCheckpoint >= chepointCount)
                {
                    nextCheckpoint = 0;
                }
            }
        }
    }
}