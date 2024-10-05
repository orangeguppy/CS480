using UnityEngine;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public QuizUIController uiController;
    private QuizAPIService apiService;
    private QuizState quizState;

    private void Start()
    {
        uiController = GetComponent<QuizUIController>();
        quizState = new QuizState();
        apiService = new QuizAPIService();
        StartCoroutine(InitializeQuiz());
    }

    private IEnumerator InitializeQuiz()
    {
        yield return StartCoroutine(apiService.FetchQuizQuestions(quizState.Subcategory, quizState.UserEmail));
        if (apiService.QuizQuestions != null && apiService.QuizQuestions.Count > 0)
        {
            quizState.SetQuizQuestions(apiService.QuizQuestions);
            uiController.InitializeUI(quizState);
        }
        else
        {
        }
    }

    public void NavigateQuestion(int direction)
    {
        quizState.NavigateQuestion(direction);
        uiController.DisplayQuestion(quizState.CurrentQuestionIndex);
    }

    public void UpdateAnswer(int optionIndex, bool isSelected)
    {
        quizState.UpdateAnswer(optionIndex, isSelected);
    }
}