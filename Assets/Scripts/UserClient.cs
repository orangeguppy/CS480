using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UserClient : MonoBehaviour
{
    // First get the fields that contain the username and password
    private static TMP_InputField username;
    private static TMP_InputField password;
    private static TMP_InputField confirmPassword;
    private static TMP_InputField recipient_email_otp_pw;

    public static IEnumerator CreateUserCoroutine()
    {
        username = GameObject.FindWithTag("NewUserUsername").GetComponent<TMP_InputField>();
        password = GameObject.FindWithTag("NewUserPassword").GetComponent<TMP_InputField>();
        confirmPassword = GameObject.FindWithTag("ConfirmPassword").GetComponent<TMP_InputField>();

        if (password.text != confirmPassword.text)
        {
            Debug.Log("The passwords don't match. User not created.");
            yield break;
        }

        Debug.Log("Username is");
        Debug.Log(username.text);
        Debug.Log(password.text);
        // Create the user data object
        UserData userData = new UserData
        {
            username = username.text,
            password = password.text,
            role = "user"
        };

        // Serialize the user data to JSON
        string jsonData = JsonUtility.ToJson(userData);

        // Set the URL of your FastAPI endpoint
        string url = "http://127.0.0.1:8000/users/";

        // Create a new UnityWebRequest for POST
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            // Set the request headers and body
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for a response
            yield return request.SendWebRequest();

            // Handle the response
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success: " + request.downloadHandler.text);
                SceneController sceneController = GameObject.Find("ButtonController").GetComponent<SceneController>();
                GameObject currentPage = sceneController.createAccPage;
                GameObject nextPage = sceneController.verifyEmailAddrPage;
                currentPage.SetActive(false);
                nextPage.SetActive(true);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }
    }

    public static IEnumerator SendOTPEmailCoroutine()
    {
        recipient_email_otp_pw = GameObject.FindWithTag("EmailForOTPPwReset").GetComponent<TMP_InputField>();

        Debug.Log("Recipient email is");
        Debug.Log(recipient_email_otp_pw.text);

        // Base URL
        string url = "http://127.0.0.1:8000/otp";

        // Create JSON payload
        OTPRequest requestData = new OTPRequest { username = recipient_email_otp_pw.text };
        string jsonData = JsonUtility.ToJson(requestData);

        // Create the UnityWebRequest
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            // Attach JSON data as raw bytes
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for a response
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                // Process the response
                Debug.Log("Response: " + request.downloadHandler.text);
            }
        }
    }

    public static IEnumerator SendPwRequestCoroutine()
    {
        TMP_InputField email = GameObject.FindWithTag("EmailForPwReset").GetComponent<TMP_InputField>();
        TMP_InputField otp = GameObject.FindWithTag("OTPForPwReset").GetComponent<TMP_InputField>();
        TMP_InputField pw = GameObject.FindWithTag("PwForPwReset").GetComponent<TMP_InputField>();
        TMP_InputField confirmPw = GameObject.FindWithTag("ConfirmPwForPwReset").GetComponent<TMP_InputField>();

        if (pw.text != confirmPw.text)
        {
            Debug.Log("The passwords don't match. Password request not sent.");
            yield break;
        }

        // Base URL
        string url = "http://127.0.0.1:8000/users/update-password";

        // Create JSON payload
        PWResetRequest requestData = new PWResetRequest { username = email.text, otp = int.Parse(otp.text), password = pw.text };
        string jsonData = JsonUtility.ToJson(requestData);

        // Create the UnityWebRequest
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            // Attach JSON data as raw bytes
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for a response
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                // Process the response
                Debug.Log("Response: " + request.downloadHandler.text);
            }
        }
    }

    public static IEnumerator ActivateAccRequestCoroutine()
    {
        TMP_InputField email = GameObject.Find("EmailForAccVerification").GetComponent<TMP_InputField>();
        TMP_InputField otp = GameObject.Find("OTPForAccVerification").GetComponent<TMP_InputField>();

        // Base URL
        string url = "http://127.0.0.1:8000/users/activate-account";

        // Create JSON payload
        AccActivationRequest requestData = new AccActivationRequest { username = email.text, otp = int.Parse(otp.text) };
        string jsonData = JsonUtility.ToJson(requestData);

        // Create the UnityWebRequest
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            // Attach JSON data as raw bytes
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for a response
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                // Process the response
                Debug.Log("Response: " + request.downloadHandler.text);

                // Destroy the current page and show the next one
                SceneController sceneController = GameObject.Find("ButtonController").GetComponent<SceneController>();
                GameObject currentPage = sceneController.verifyEmailAddrPage;
                GameObject nextPage = sceneController.registrationCompletePage;
                currentPage.SetActive(false);
                nextPage.SetActive(true);
            }
        }
    }
}