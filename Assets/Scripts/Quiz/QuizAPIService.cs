using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class QuizAPIService
{
    private const string API_URL = "http://localhost:8000/api/v1/quizzes";

    public List<QuizQuestion> QuizQuestions { get; private set; }
    public QuizResult QuizResult { get; private set; }

    public IEnumerator FetchQuizQuestions(string subcategory, string userEmail)
    {
        string url = $"{API_URL}/generate-quiz/{subcategory}?user_email={userEmail}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                QuizQuestions = JsonConvert.DeserializeObject<List<QuizQuestion>>(json);
            }
        }
    }

    public IEnumerator SubmitQuiz(string subcategory, string userEmail, List<List<string>> userAnswers)
    {
        string url = $"{API_URL}/submit-quiz/{subcategory}?user_email={userEmail}";
        QuizSubmission submission = new QuizSubmission { answers = userAnswers };
        string json = JsonConvert.SerializeObject(submission);

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, json))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                string responseText = request.downloadHandler.text;
                QuizResult = JsonConvert.DeserializeObject<QuizResult>(responseText);
            }
        }
    }
}