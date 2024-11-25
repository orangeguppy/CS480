using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

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
        PopUpController popUpController = FindObjectOfType<PopUpController>();
    
        if (sceneNav == null)
        {
            sceneNav = GameObject.Find("Canvas").GetComponent<SceneNav>();
        }

        // Use UnityWebRequest for HTTP requests
        var formData = new WWWForm();
        formData.AddField("username", username.text);
        formData.AddField("password", password.text);

        // Save the email for autofilling
        PlayerPrefs.SetString("Email", username.text);
        PlayerPrefs.Save();

        Debug.Log("HERE");
        Debug.Log(username.text);
        Debug.Log(password.text);
        using (UnityWebRequest request = UnityWebRequest.Post("https://phishfindersrealforrealsbs.org/auth/login/token", formData))
        // using (UnityWebRequest request = UnityWebRequest.Post("http://132.147.102.248//auth/login/token", formData))
        {
            Debug.Log("Sending request now");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                popUpController.ShowPopup("green", "Success", "Login successful!");
                Debug.Log("Success");

                string jsonResponse = request.downloadHandler.text;
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);
                Debug.Log("Token: " + response.access_token);
                Debug.Log("User ID: " + response.token_type);
                // Save the access token to PlayerPrefs, and session ID too
                PlayerPrefs.SetString("AccessToken", response.access_token);
                PlayerPrefs.SetString("Email", username.text);
                PlayerPrefs.Save();

                // Save session data to local storage
                SaveSessionData(response.session);

                sceneNav.loadMainMenu();
            }
            else if (request.responseCode == 403)
            {
                string res = request.downloadHandler.text;
                HTTPResponse httpRes = JsonUtility.FromJson<HTTPResponse>(res);
                res = httpRes.detail;
                Debug.LogError($"Login failed, unauthorised: {request.downloadHandler.text}");
                popUpController.ShowPopup("red", "Error", res);
                SceneController sceneController = GameObject.Find("ButtonController").GetComponent<SceneController>();
                GameObject loginPage = GameObject.Find("SignInPage");
                GameObject emailVerificationPage = sceneController.verifyEmailAddrPage;
                loginPage.SetActive(false);
                emailVerificationPage.SetActive(true);
            }
            else
            {
                string res = request.downloadHandler.text;
                Debug.Log(res);
                HTTPResponse httpRes = JsonUtility.FromJson<HTTPResponse>(res);
                res = httpRes.detail;
                Debug.LogError($"Login failed other: {request.downloadHandler.text}");
                popUpController.ShowPopup("red", "Error", res);
            }
        }
    }

    private static void SaveSessionData(SessionData sessionData)
    {
        // Store session information in PlayerPrefs
        PlayerPrefs.SetString("session_id", sessionData.session_id);
        PlayerPrefs.SetString("username", sessionData.username);
        PlayerPrefs.SetString("created_at", sessionData.created_at.ToString("o")); // ISO 8601 format
        PlayerPrefs.SetString("expires_at", sessionData.expires_at.ToString("o")); // ISO 8601 format
        PlayerPrefs.Save();

        Debug.Log($"Session saved: {sessionData.session_id}, Username: {sessionData.username}, Expires At: {sessionData.expires_at}");
    }
}