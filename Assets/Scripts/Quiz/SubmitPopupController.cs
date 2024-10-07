using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubmitPopupController : MonoBehaviour
{
    public GameObject ScoreUI;
    public Button submitButton;
    public GameObject popupPrefab;
    public TextMeshProUGUI scoreText;
    public QuizScoreUIHandler quizScoreUIHandler;
    public QuizManager quizManager;

    public void ShowUI()
    {
        popupPrefab.SetActive(true);
    }

    public void HideUI()
    {
        popupPrefab.SetActive(false);
    }

    // public void showScoreUI()
    // {
    //     ScoreUI.SetActive(true);
    //     HideUI();
    // }

    public void showScoreUI(int score)
    {
        ScoreUI.SetActive(true);
        scoreText.text = $"Your Score: {score}/15";
        HideUI();
    }

}