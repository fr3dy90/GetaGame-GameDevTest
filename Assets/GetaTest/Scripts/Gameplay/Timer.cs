using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private NotifyTime noTime;
    public float timeToAdd;

    private void Start()
    {
        noTime = transform.parent.GetComponent<NotifyTime>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
           noTime.SendTime(timeToAdd);
        }
    }
}

