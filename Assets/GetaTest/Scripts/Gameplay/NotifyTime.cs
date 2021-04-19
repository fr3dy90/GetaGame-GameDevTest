using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyTime : MonoBehaviour
{
    public delegate void getTime(float time);
    public event getTime onGetTime;

    public void SendTime(float time)
    {
        onGetTime(time);
    }
}
