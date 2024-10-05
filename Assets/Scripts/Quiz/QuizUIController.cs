using UnityEngine;
using UnityEngine.UI;

public class QuizUIController : MonoBehaviour
{
    public Text questionNumberText;
    public Text questionText;
    public Toggle[] optionToggles;
    public Button submitButton;
    public Button leftButton;
    public Button rightButton;
    public GameObject submitPopup;
    public Button yesButton;
    public Button noButton;

    private QuizManager quizManager;
    private QuizState quizState;

    private void Start()
    {
        quizManager = GetComponent<QuizManager>();
        SetupListeners();
    }

    public void InitializeUI(QuizState state)
    {
        quizState = state;
        DisplayQuestion(0);
    }

    public void DisplayQuestion(int index)
    {
        QuizQuestion question = quizState.GetCurrentQuestion();
        questionNumberText.text = $"Q{index + 1}";
        questionText.text = question.question_text;

        for (int i = 0; i < optionToggles.Length; i++)
        {
            optionToggles[i].GetComponentInChildren<Text>().text = question.GetOption(i);
            optionToggles[i].isOn = quizState.IsOptionSelected(index, i);
            optionToggles[i].group = question.correct_answer.Count > 1 ? null : optionToggles[0].group;
        }

        leftButton.interactable = index > 0;
        rightButton.interactable = index < quizState.TotalQuestions - 1;
    }

    private void SetupListeners()
    {
        submitButton.onClick.AddListener(ShowSubmitPopup);
        leftButton.onClick.AddListener(() => quizManager.NavigateQuestion(-1));
        rightButton.onClick.AddListener(() => quizManager.NavigateQuestion(1));
        yesButton.onClick.AddListener(quizManager.SubmitQuiz);
        noButton.onClick.AddListener(CloseSubmitPopup);

        for (int i = 0; i < optionToggles.Length; i++)
        {
            int index = i;
            optionToggles[i].onValueChanged.AddListener((bool isOn) => quizManager.UpdateAnswer(index, isOn));
        }
    }

    private void ShowSubmitPopup()
    {
        submitPopup.SetActive(true);
    }

    private void CloseSubmitPopup()
    {
        submitPopup.SetActive(false);
    }

    public void DisplayQuizResult(QuizResult result)
    {
        // Implement result display logic here
        Debug.Log($"Quiz Score: {result.score}, Improved: {result.improved}");
    }
}