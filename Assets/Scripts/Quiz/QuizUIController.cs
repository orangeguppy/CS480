using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuizUIController : MonoBehaviour
{
    public TextMeshProUGUI questionNumberText;
    public TextMeshProUGUI questionText;
    public Toggle[] optionToggles;
    public Button leftButton;
    public Button rightButton;
    public Button submitButton;
    private QuizManager quizManager;
    private QuizState quizState;
    public TextMeshProUGUI timerText;

    [SerializeField] private GameObject quizContent;
    [SerializeField] private CanvasGroup quizCanvasGroup;
    [SerializeField] private float fadeInDuration = 0.5f;

    private void Start()
    {
        if (quizContent != null)
        {
            quizContent.SetActive(false);
        }
        quizManager = GetComponent<QuizManager>();
        SetupListeners();
    }

    public void InitializeUI(QuizState state)
    {
        quizState = state;
        if (quizContent != null)
        {
            quizContent.SetActive(true);
            StartCoroutine(FadeInContent());
        }
        DisplayQuestion(0);
    }

    private IEnumerator FadeInContent()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            if (quizCanvasGroup != null)
            {
                quizCanvasGroup.alpha = newAlpha;
            }
            yield return null;
        }

        if (quizCanvasGroup != null)
        {
            quizCanvasGroup.alpha = 1f;
        }
    }

    public void DisplayQuestion(int index)
    {
        QuizQuestion question = quizState.GetCurrentQuestion();
        questionNumberText.text = $"Q{index + 1}";
        questionText.text = question.question_text;

        for (int i = 0; i < optionToggles.Length; i++)
        {
            optionToggles[i].GetComponentInChildren<TextMeshProUGUI>().text = question.GetOption(i);
            optionToggles[i].isOn = quizState.IsOptionSelected(index, i);
            optionToggles[i].group = question.correct_answer.Count > 1 ? null : optionToggles[0].group;
        }

        leftButton.interactable = index > 0;
        rightButton.interactable = index < quizState.TotalQuestions - 1;
    }

    private void SetupListeners()
    {
        leftButton.onClick.AddListener(() => quizManager.NavigateQuestion(-1));
        rightButton.onClick.AddListener(() => quizManager.NavigateQuestion(1));

        for (int i = 0; i < optionToggles.Length; i++)
        {
            int index = i;
            optionToggles[i].onValueChanged.AddListener((bool isOn) => quizManager.UpdateAnswer(index, isOn));
        }

        submitButton.onClick.AddListener(() => quizManager.SubmitQuiz());
    }
    public void UpdateTimerDisplay(string time)
    {
        if (timerText != null)
        {
            timerText.text = time;
        }
    }
}