using UnityEngine;
using System;

public class QuizTimer : MonoBehaviour
{
    public event Action OnTimerEnd;
    [SerializeField] private float totalTime = 1200f;
    public float RemainingTime { get; private set; }
    public float TotalTime => totalTime;

    private bool isRunning;
    private bool isPaused;

    public void StartTimer()
    {
        RemainingTime = totalTime;
        isRunning = true;
        isPaused = false;
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    private void Update()
    {
        if (isRunning && !isPaused && RemainingTime > 0)
        {
            RemainingTime -= Time.deltaTime;
            if (RemainingTime <= 0)
            {
                RemainingTime = 0;
                isRunning = false;
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