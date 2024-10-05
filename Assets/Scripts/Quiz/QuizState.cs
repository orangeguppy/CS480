using System.Collections.Generic;

public class QuizState
{
    public string Subcategory { get; set; } = "SSRF"; // Default value, change as needed
    public string UserEmail { get; set; } = "test@email.com"; // Default value, change as needed

    public List<QuizQuestion> QuizQuestions { get; private set; }
    public List<List<string>> UserAnswers { get; private set; }
    public int CurrentQuestionIndex { get; private set; }
    public int TotalQuestions => QuizQuestions?.Count ?? 0;

    public void SetQuizQuestions(List<QuizQuestion> questions)
    {
        if (questions == null || questions.Count == 0)
        {
            QuizQuestions = new List<QuizQuestion>();
            UserAnswers = new List<List<string>>();
            CurrentQuestionIndex = 0;
        }
        else
        {
            QuizQuestions = questions;
            UserAnswers = new List<List<string>>(new List<string>[questions.Count]);
            CurrentQuestionIndex = 0;
        }
    }

    public QuizQuestion GetCurrentQuestion()
    {
        if (QuizQuestions == null || QuizQuestions.Count == 0 || CurrentQuestionIndex < 0 || CurrentQuestionIndex >= QuizQuestions.Count)
        {
            return null;
        }
        return QuizQuestions[CurrentQuestionIndex];
    }

    public void NavigateQuestion(int direction)
    {
        if (QuizQuestions.Count == 0)
        {
            return;
        }

        CurrentQuestionIndex = (CurrentQuestionIndex + direction + TotalQuestions) % TotalQuestions;
    }

    public void UpdateAnswer(int optionIndex, bool isSelected)
    {
        if (UserAnswers[CurrentQuestionIndex] == null)
        {
            UserAnswers[CurrentQuestionIndex] = new List<string>();
        }

        string option = $"option_{optionIndex + 1}";
        if (isSelected && !UserAnswers[CurrentQuestionIndex].Contains(option))
        {
            UserAnswers[CurrentQuestionIndex].Add(option);
        }
        else if (!isSelected)
        {
            UserAnswers[CurrentQuestionIndex].Remove(option);
        }
    }

    public bool IsOptionSelected(int questionIndex, int optionIndex)
    {
        return UserAnswers[questionIndex]?.Contains($"option_{optionIndex + 1}") ?? false;
    }
}