using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    public TextMeshProUGUI timeText;

    public int currentHour = 9;
    public int currentMinute = 0;

    public float timePerGameTick = 90f;
    public float timeScale = 1f;
    private float timer;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        timeText.text = $"{currentHour:D2}:{currentMinute:D2}";
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timePerGameTick / timeScale)
        {
            AdvanceTime();
            timer = 0f;
        }
    }

    private void AdvanceTime()
    {
        currentMinute += 10;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;

            if (currentHour >= 24) currentHour = 0;
        }

        timeText.text = $"{currentHour:D2}:{currentMinute:D2}";
    }
}
