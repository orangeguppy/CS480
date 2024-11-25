using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logout : MonoBehaviour
{
    // Start is called before the first frame update
    void LogoutUser()
    {
        // Clear access token, clear saved user data, clear email, clear session id
        PlayerPrefs.DeleteKey("AccessToken");
        PlayerPrefs.Save(); // Ensures changes are written to disk
        Debug.Log("User logged out.");

        // Show popup

    }
}