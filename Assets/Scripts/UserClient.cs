using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;

public class UserClient : MonoBehaviour
{
    // First get the fields that contain the username and password
    private static TMP_InputField username;
    private static TMP_InputField password;
    private static TMP_InputField confirmPassword;
    private static TMP_InputField recipient_email_otp_pw;

    // Regular expression for validating an email address
    private static readonly string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public static bool ValidateEmail(string email)
    {
        PopUpController popUpController = FindObjectOfType<PopUpController>();

        // Reject null strings
        if (string.IsNullOrWhiteSpace(email))
        {
            popUpController.ShowPopup("red", "Error", "Please enter an email address");
            return false;
        }

        // Check if the email address is valid
        bool isValid = Regex.IsMatch(email, emailPattern);
        if (isValid == false)
        {
            popUpController.ShowPopup("red", "Error", "Please enter a valid email address");
        }
        return isValid;
    }

    public static bool ValidatePassword(string password)
    {
        Debug.Log(password);
        PopUpController popUpController = FindObjectOfType<PopUpController>();

        // Reject null strings
        if (string.IsNullOrWhiteSpace(password))
        {
            popUpController.ShowPopup("red", "Error", "Please enter a password");
            return false;
        }

        // Define password strength criteria:
        // - At least 8 characters long
        // - Contains at least 1 uppercase letter
        // - Contains at least 1 lowercase letter
        // - Contains at least 1 digit
        // - Contains at least 1 special character (!@#$%^&*)
        if (password.Length < 8)
        {
            popUpController.ShowPopup("red", "Error", "Password must be at least 8 characters long");
            return false;
        }

        if (Regex.IsMatch(password, @"[A-Z]") == false || Regex.IsMatch(password, @"[a-z]") == false)
        {
            popUpController.ShowPopup("red", "Error", "Password must have at least one upper case and one lower case alphabet character");
            return false;
        }

        if (Regex.IsMatch(password, @"\d") == false)
        {
            popUpController.ShowPopup("red", "Error", "Password must have at least one digit");
            return false;
        }

        if (Regex.IsMatch(password, @"[\W_]") == false)
        {
            popUpController.ShowPopup("red", "Error", "Password must have at least one special character");
            return false;
        } 
        return true;
    }

    public static IEnumerator CreateUserCoroutine()
    {
        PopUpController popUpController = FindObjectOfType<PopUpController>();
        username = GameObject.FindWithTag("NewUserUsername").GetComponent<TMP_InputField>();
        password = GameObject.FindWithTag("NewUserPassword").GetComponent<TMP_InputField>();
        confirmPassword = GameObject.FindWithTag("ConfirmPassword").GetComponent<TMP_InputField>();

        if (password.text != confirmPassword.text)
        {
            popUpController.ShowPopup("red", "Error", "The passwords don't match. User not created");
            yield break;
        }

        // Validate email and password
        bool emailIsValid = ValidateEmail(username.text);
        bool pwIsValid = ValidatePassword(password.text);

        if (emailIsValid == false || pwIsValid == false)
        {
            yield break;
        }

        // Don't create a user if any are false
        // Debug.Log("Username is");
        // Debug.Log(username.text);
        // Debug.Log(password.text);

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
                // Save details to autofill
                PlayerPrefs.SetString("Email", username.text);
                Debug.Log("Success: " + request.downloadHandler.text);
                SceneController sceneController = GameObject.Find("ButtonController").GetComponent<SceneController>();
                GameObject currentPage = sceneController.createAccPage;
                GameObject nextPage = sceneController.verifyEmailAddrPage;
                currentPage.SetActive(false);
                nextPage.SetActive(true);
            }
            else
            {
                string res = request.downloadHandler.text;
                HTTPResponse httpRes = JsonUtility.FromJson<HTTPResponse>(res);
                res = httpRes.detail;
                Debug.Log("Error: " + res);
                popUpController.ShowPopup("red", "Error", res);
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
        OTPRequest requestData = new OTPRequest { username = recipient_email_otp_pw.text, new_acc=false };
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
                string res = request.downloadHandler.text;
                HTTPResponse httpRes = JsonUtility.FromJson<HTTPResponse>(res);
                res = httpRes.detail;
                Debug.LogError("Error: " + res);
                PopUpController popUpController = FindObjectOfType<PopUpController>();
                popUpController.ShowPopup("red", "Error", res);
                // Do not go to the next screen
            }
            else
            {
                // Process the response
                string res = request.downloadHandler.text;
                HTTPResponse httpRes = JsonUtility.FromJson<HTTPResponse>(res);
                res = httpRes.detail;
                Debug.Log("Response: " + res);
                // Go to the next screen
                SceneController sceneController = GameObject.Find("ButtonController").GetComponent<SceneController>();
                GameObject currentPage = null;
                GameObject nextPage = null;
                if (sceneController != null && sceneController.sendOTP != null)
                {
                    currentPage = sceneController.sendOTP;
                }
                else
                {
                    MainMenuManager mainMenuManager = GameObject.Find("ButtonController").GetComponent<MainMenuManager>();
                    currentPage = mainMenuManager.sendOTP;
                }
                
                if (sceneController != null && sceneController.resetPwPage != null)
                {
                    nextPage = sceneController.resetPwPage;
                }
                else
                {
                    MainMenuManager mainMenuManager = GameObject.Find("ButtonController").GetComponent<MainMenuManager>();
                    nextPage = mainMenuManager.resetPwPage;
                }
                currentPage.SetActive(false);
                nextPage.SetActive(true);

            }
        }
    }

    public static IEnumerator SendPwRequestCoroutine()
    {
        TMP_InputField email = GameObject.FindWithTag("EmailForPwReset").GetComponent<TMP_InputField>();
        TMP_InputField otp = GameObject.FindWithTag("OTPForPwReset").GetComponent<TMP_InputField>();
        TMP_InputField pw = GameObject.FindWithTag("PwForPwReset").GetComponent<TMP_InputField>();
        TMP_InputField confirmPw = GameObject.FindWithTag("ConfirmPwForPwReset").GetComponent<TMP_InputField>();

        PopUpController popUpController = FindObjectOfType<PopUpController>();

        if (pw.text != confirmPw.text)
        {
            popUpController.ShowPopup("red", "Error", "The passwords don't match. User not created");
            Debug.Log("The passwords don't match. Password request not sent.");
            yield break;
        }

        // Validate email and password
        bool emailIsValid = ValidateEmail(email.text);
        bool pwIsValid = ValidatePassword(pw.text);

        if (emailIsValid == false || pwIsValid == false)
        {
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
                string res = request.downloadHandler.text;
                HTTPResponse httpRes = JsonUtility.FromJson<HTTPResponse>(res);
                res = httpRes.detail;
                Debug.LogError("Error: " + res);
                popUpController.ShowPopup("red", "Error", res);
            }
            else
            {
                // Process the response
                Debug.Log("Pw Response: " + request.downloadHandler.text);
                popUpController.ShowPopup("green", "Success", "Password changed!");
                // Destroy the current page and show the next one
                SceneController sceneController = GameObject.Find("ButtonController").GetComponent<SceneController>();
                
                if(sceneController == null) {
                    MainMenuManager sceneController2 = GameObject.Find("ButtonController").GetComponent<MainMenuManager>();
                     GameObject currentPage2 = sceneController2.sendOTP;
                    GameObject nextPage2 = sceneController2.resetPwPage;
                    currentPage2.SetActive(false);
                    nextPage2.SetActive(true);
                }

                if(sceneController != null) {
                    GameObject currentPage = sceneController.sendOTP;
                    GameObject nextPage = sceneController.resetPwPage;
                    currentPage.SetActive(false);
                    nextPage.SetActive(true);
                }
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
            // Create pop up
            PopUpController popUpController = FindObjectOfType<PopUpController>();
            // Attach JSON data as raw bytes
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for a response
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result != UnityWebRequest.Result.Success)
            {
                string res = request.downloadHandler.text;
                HTTPResponse httpRes = JsonUtility.FromJson<HTTPResponse>(res);
                res = httpRes.detail;
                Debug.LogError("Error: " + res);
                popUpController.ShowPopup("red", "Error", res);
            }
            else
            {
                // Process the response
                Debug.Log("Pw Response: " + request.downloadHandler.text);
                popUpController.ShowPopup("green", "Success", "Account activated!");

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