using System;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [Header("Sun Color")]
    public Gradient sunColorGradient;

    [Header("Time Settings")]
    public int startHour = 8;
    public int startMinute = 0;
    public float realSecondsPerGameMinute = 0.04f;

    [Header("UI")]
    public TextMeshProUGUI timeText;

    [Header("Sun Settings")]
    public Light sunLight;
    public float sunYRotation = 170f;

    [Header("Moon Settings")]
    public Transform moonTransform;
    public Renderer moonRenderer;
    public float moonDistance = 300f;

    private int currentHour;
    private int currentMinute;
    private float timer;
    private bool wasNight;

    public int CurrentHour => currentHour;
    public int CurrentMinute => currentMinute;
    public bool IsNight => currentHour >= 18 || currentHour < 6;

    public event Action OnDayStarted;
    public event Action OnNightStarted;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentHour = startHour;
        currentMinute = startMinute;
        wasNight = IsNight;

        UpdateTimeUI();
        UpdateSunAndMoonSmooth();
    }

    void Update()
    {
        timer += Time.deltaTime;

        while (timer >= realSecondsPerGameMinute)
        {
            timer -= realSecondsPerGameMinute;
            AddMinute();
        }

        UpdateSunAndMoonSmooth();
    }

    void AddMinute()
    {
        currentMinute++;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;

            if (currentHour >= 24)
                currentHour = 0;
        }

        UpdateTimeUI();
    }

    void UpdateTimeUI()
    {
        if (timeText != null)
            timeText.text = $"{currentHour:00}:{currentMinute:00}";
    }

    void UpdateSunAndMoonSmooth()
    {
        if (sunLight == null) return;

        float currentTotalMinutes = currentHour * 60f + currentMinute;
        float smoothMinutes = currentTotalMinutes + (timer / realSecondsPerGameMinute);
        float timePercent = smoothMinutes / (24f * 60f);

        // 해 회전
        float sunXRotation = timePercent * 360f - 90f;
        sunLight.transform.rotation = Quaternion.Euler(sunXRotation, sunYRotation, 0f);

        

        // 해 색
        if (sunColorGradient != null)
        {
            sunLight.color = sunColorGradient.Evaluate(timePercent);
        }

        // 달 위치
        if (moonTransform != null)
        {
            Vector3 center = transform.position;

            Vector3 moonDirection = sunLight.transform.forward.normalized;

            moonTransform.position = center + moonDirection * moonDistance;
        }

    }
}