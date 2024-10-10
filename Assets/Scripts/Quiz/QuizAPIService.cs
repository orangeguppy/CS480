using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class QuizAPIService
{
    private const string API_URL = "http://localhost:8001/api/v1";

    public List<QuizQuestion> QuizQuestions { get; private set; }

    public IEnumerator FetchQuizQuestions(string subcategory, string userEmail)
    {
        string url = $"{API_URL}/generate-quiz/{subcategory}?user_email={userEmail}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                // Debug.LogError("Error: " + request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                QuizQuestions = JsonConvert.DeserializeObject<List<QuizQuestion>>(json);
            }
        }
    }
}