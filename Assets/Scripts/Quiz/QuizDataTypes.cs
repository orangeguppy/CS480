using System.Collections.Generic;

[System.Serializable]
public class QuizQuestion
{
    public string category;
    public string sub;
    public string question_text;
    public string option_1;
    public string option_2;
    public string option_3;
    public string option_4;
    public List<string> correct_answer;

    public string GetOption(int index)
    {
        return index switch
        {
            0 => option_1,
            1 => option_2,
            2 => option_3,
            3 => option_4,
            _ => string.Empty,
        };
    }
}

[System.Serializable]
public class QuizSubmission
{
    public List<List<string>> answers;
}

[System.Serializable]
public class QuizResult
{
    public int score;
    public bool improved;
}