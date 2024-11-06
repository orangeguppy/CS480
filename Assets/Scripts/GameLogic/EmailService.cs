using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;

public class EmailService
{
    private const string API_URL = "http://localhost:8000/api/v1";
    public EmailData CurrentEmail { get; private set; }

    public IEnumerator FetchEmail()
    {
        string url = $"{API_URL}/minigame";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                // Debug.LogError($"Error: {request.error}");
            }
            else
            {
                string json = request.downloadHandler.text;
                EmailResponse response = JsonConvert.DeserializeObject<EmailResponse>(json);
                CurrentEmail = response.ToEmailData();
            }
        }
    }
}