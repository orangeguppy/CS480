using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailManager : MonoBehaviour
{
    [SerializeField] private EmailController emailController;
    private EmailService emailService;
    private int correctAnswers = 0;
    private int totalAnswers = 0;

    private void Awake()
    {
        emailService = new EmailService();
        emailController.Initialize(this);
    }

    // Called by external button
    public void LoadNewEmail()
    {
        StartCoroutine(FetchAndDisplayEmail());
    }

    private IEnumerator FetchAndDisplayEmail()
    {
        yield return StartCoroutine(emailService.FetchEmail());

        if (emailService.CurrentEmail != null)
        {
            emailController.DisplayEmail(emailService.CurrentEmail);
            emailController.ShowEmailMinigame();
        }
        else
        {
            // Debug.LogError("Failed to fetch email");
        }
    }

    public void HandleAnswer(bool answerIsFake)
    {
        totalAnswers++;
        if (answerIsFake == emailService.CurrentEmail.is_modified)
        {
            correctAnswers++;
        }

        // Debug.Log($"Score: {correctAnswers}/{totalAnswers}");
        emailController.HideEmailMinigame();
    }

    public int GetCorrectAnswers() => correctAnswers;
    public int GetTotalAnswers() => totalAnswers;
}