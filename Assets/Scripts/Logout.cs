using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour
{
    // Start is called before the first frame update
    public static void LogoutUser()
    {
        // Clear access token, clear saved user data, clear email, clear session id
        PlayerPrefs.DeleteKey("AccessToken");
        PlayerPrefs.DeleteKey("SessionID");
        PlayerPrefs.Save(); // Ensures changes are written to disk
        Debug.Log("User logged out.");

        // Go to login page
        SceneManager.LoadScene("Login");
    }
}