using UnityEngine;
using System;

public class QuizTimer : MonoBehaviour
{
    public event Action OnTimerEnd;
    [SerializeField] private float totalTime = 120f;
    public float TotalTime => totalTime;
    public float RemainingTime { get; private set; }

    private bool isRunning = false;

    public void StartTimer()
    {
        RemainingTime = TotalTime;
        isRunning = true;
    }

    private void Update()
    {
        if (isRunning)
        {
            RemainingTime -= Time.deltaTime;
            if (RemainingTime <= 0)
            {
                isRunning = false;
                RemainingTime = 0;
                OnTimerEnd?.Invoke();
            }
        }
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(RemainingTime / 60);
        int seconds = Mathf.FloorToInt(RemainingTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}