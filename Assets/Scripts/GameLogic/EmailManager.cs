using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailManager : MonoBehaviour
{
    private EmailService emailService;
    private EmailController emailController;
    private int correctAnswers = 0;
    private int totalAnswers = 0;
    private bool isLoading = false;

    private void Awake()
    {
        Debug.Log("[EmailManager] Awake called");
        emailService = new EmailService();
        emailController = GetComponent<EmailController>();

        if (emailController == null)
        {
            Debug.LogError("[EmailManager] EmailController component not found!");
        }
    }

    public void LoadNewEmail()
    {
        if (isLoading)
        {
            Debug.Log("[EmailManager] Already loading email, skipping request");
            return;
        }

        Debug.Log("[EmailManager] LoadNewEmail called");
        StartCoroutine(FetchAndDisplayEmail());
    }

    private IEnumerator FetchAndDisplayEmail()
    {
        isLoading = true;
        Debug.Log("[EmailManager] Starting to fetch email");

        yield return StartCoroutine(emailService.FetchEmail());

        Debug.Log("[EmailManager] Fetch completed");
        if (emailService.CurrentEmail != null)
        {
            Debug.Log("[EmailManager] Email received, displaying...");
            emailController.DisplayEmail(emailService.CurrentEmail);
        }
        else
        {
            Debug.LogError("[EmailManager] Failed to fetch email: CurrentEmail is null");
        }

        isLoading = false;
    }

    public void HandleAnswer(bool answerIsFake)
    {
        if (emailService.CurrentEmail == null)
        {
            Debug.LogError("[EmailManager] Trying to handle answer but CurrentEmail is null!");
            return;
        }

        totalAnswers++;
        if (answerIsFake != emailService.CurrentEmail.is_modified)
        {
            correctAnswers++;
            Debug.Log("correct ans");
            PlayerInfo.EndlessScore = Mathf.RoundToInt((float)(PlayerInfo.EndlessScore  * 1.2));
        }
        else
        {
            PlayerInfo.EndlessScore = Mathf.RoundToInt((float)(PlayerInfo.EndlessScore * 0.8));
        }
        Debug.Log($"[EmailManager] Score: {correctAnswers}/{totalAnswers}");
    }
}