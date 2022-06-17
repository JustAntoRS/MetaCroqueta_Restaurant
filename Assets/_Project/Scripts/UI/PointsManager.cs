using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PointsManager : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private float _points = 0;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void PointsIncreasedEventHandler(int points)
    {
        _points += points;
        _text.SetText($"Points:{System.Environment.NewLine}{_points}");
        Debug.Log($"Current points being displayed -> {_points}");
    }

    public void GameStatusChangeEventHandler(GameStatus status)
    { 
        if (status == GameStatus.GameStarted)
        {
            Restart();
        }
    }

    private void Restart()
    {
        _points = 0;
        _text.SetText($"Points:{System.Environment.NewLine}{_points}");
    }
}
