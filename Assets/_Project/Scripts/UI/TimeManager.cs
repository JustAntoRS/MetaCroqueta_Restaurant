using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimeManager : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private bool isActive = false;
    private float lastTime;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isActive)
        {
            var time = TimeSpan.FromSeconds(Time.time - lastTime);
            _text.SetText($"{time.Minutes:00}:{time.Seconds:00}");   
        }
    }
    
    public void GameStatusChangeEventHandler(GameStatus status)
    {
        if (status == GameStatus.GameWaiting)
        {
            StopTimer();
        }
        else if (status == GameStatus.GameStarted)
        {
            StartTimer();
        }
    }

    private void StartTimer()
    {
        isActive = true;
        lastTime = Time.time;
    }

    private void StopTimer()
    {
        isActive = false;
    }
    
    
}
