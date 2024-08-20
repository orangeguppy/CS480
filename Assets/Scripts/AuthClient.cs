using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class AuthClient : MonoBehaviour
{
    private static SceneNav sceneNav;
    // First get the fields that contain the username and password
    private static TMP_InputField username;
    private static TMP_InputField password;

    public static IEnumerator Login()
    {
        username = GameObject.FindWithTag("Username").GetComponent<TMP_InputField>();
        password = GameObject.FindWithTag("Password").GetComponent<TMP_InputField>();
        if (sceneNav == null)
        {
            sceneNav = GameObject.Find("Canvas").GetComponent<SceneNav>();
        }

        // Use UnityWebRequest for HTTP requests
        var formData = new WWWForm();
        formData.AddField("username", username.text);
        formData.AddField("password", password.text);

        using (UnityWebRequest request = UnityWebRequest.Post("http://127.0.0.1:8000/auth/login/token", formData))
        {
            Debug.Log("Sending request now");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Login failed: {request.error}");
                yield break;
            }
            else
            {
                Debug.Log("Success");

                string jsonResponse = request.downloadHandler.text;
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);
                Debug.Log("Token: " + response.access_token);
                Debug.Log("User ID: " + response.token_type);
                sceneNav.loadMainMenu();
            }
        }
    }
}