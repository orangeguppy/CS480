using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubmitPopupController : MonoBehaviour
{
    public GameObject popupPrefab;
    public GameObject ScoreUI;
    public Button yesButton;
    public Button noButton;
    public TextMeshProUGUI scoreText;
    private QuizManager quizManager;

    private void Start()
    {
        quizManager = GetComponent<QuizManager>();
        if (yesButton != null)
            yesButton.onClick.AddListener(OnYesClicked);
        if (noButton != null)
            noButton.onClick.AddListener(OnNoClicked);
    }

    public void ShowConfirmationPopup()
    {
        popupPrefab.SetActive(true);
    }

    public void HideConfirmationPopup()
    {
        popupPrefab.SetActive(false);
    }

    public void ShowScoreUI(int score)
    {
        ScoreUI.SetActive(true);
        scoreText.text = $"Your Score: {score}/15";
    }

    private void OnYesClicked()
    {
        HideConfirmationPopup();
        quizManager.OnSubmitConfirmed();
    }

    private void OnNoClicked()
    {
        HideConfirmationPopup();
        quizManager.OnSubmitCancelled();
    }
}