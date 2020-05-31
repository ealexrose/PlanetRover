using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public GameObject partner;
    public float timerDelay;
    public float activeTimer;
    public float index;
    public Timer(float _timerDelay, float _activeTimer, float _index)
    {

        timerDelay = _timerDelay;
        activeTimer = _activeTimer;
        index = _index;

    }
    public Timer(float _timerDelay, float _activeTimer)
    {

        timerDelay = _activeTimer;
        activeTimer = _activeTimer;
        index = 0;
    }
    public Timer()
    {
        timerDelay = 0; ;
        activeTimer = 0;
        index = 0;
    }

    public void ResizePartner(float _newSize)
    {
        partner.transform.localScale = new Vector3(_newSize, _newSize, 1);
    }

}
