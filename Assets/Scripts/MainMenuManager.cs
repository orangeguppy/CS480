using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject gameSettings;
    public GameObject accountSettings;
    public GameObject sendOTP;
    public GameObject resetPwPage;


    public TMP_InputField teamInput;
    public TMP_Dropdown teamDropdown;
    public TMP_Dropdown deptDropdown;


    void Start()
    {
        StartCoroutine(GetUserData());
    }

    IEnumerator GetUserData()
    {
        string userEmail = PlayerPrefs.GetString("Email");
        string url = $"https://phishfindersrealforrealsbs.org/api/v1/users/email/{userEmail}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest(); // Coroutine waits for the web request to complete

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Parse the response data
                string json = request.downloadHandler.text;
                UserInLeaderboardDB user = JsonConvert.DeserializeObject<UserInLeaderboardDB>(json);

                // Handle the retrieved team and department
                if (user != null)
                {
                    // Use user.team and user.department as needed
                    PlayerPrefs.SetInt("userID", user.user_id);
                    PlayerPrefs.Save();
                }
                else
                {
                    Debug.LogWarning("User not found or invalid response.");
                }
            }
            else
            {
                Debug.LogError($"Error retrieving user data: {request.error}");
            }
        }
    }


    public void JoinTeam()
    {
        
        string teamName = teamDropdown.options[teamDropdown.value].text;

        if (!string.IsNullOrEmpty(teamName))
        {
            PlayerPrefs.SetString("Team", teamName); 
            PlayerPrefs.Save();
            StartCoroutine(UpdateUserTeam(teamName));
        }
        else
        {
            Debug.LogWarning("Team empty");
        }

    }

    public void CreateTeam()
    {
        string teamName = teamInput.text;

        if (!string.IsNullOrEmpty(teamName))
        {
            PlayerPrefs.SetString("Team", teamName);
            PlayerPrefs.Save();
            StartCoroutine(UpdateUserTeam(teamName));
        }
        else
        {
            Debug.LogWarning("Team empty");
        }

        // Update using the API

    }

    public void JoinDept()
    {
        string deptName = deptDropdown.options[deptDropdown.value].text;

        if (!string.IsNullOrEmpty(deptName))
        {
            PlayerPrefs.SetString("Department", deptName);
            PlayerPrefs.Save();
            StartCoroutine(UpdateUserDepartment(deptName));
        }
        else
        {
            Debug.LogWarning("Dept empty");
        }
    }

    IEnumerator UpdateUserTeam(string newTeam)
    {
        int userId = PlayerPrefs.GetInt("userID");
        Debug.Log($"User ID: {userId}, New Team: {newTeam}");

        // Base URL of the API endpoint
        string baseUrl = $"https://phishfindersrealforrealsbs.org/api/v1/leaderboard/individual/team/{userId}?new_team={newTeam}";

        // Create the UnityWebRequest for a PUT request
        using (UnityWebRequest request = UnityWebRequest.Put(baseUrl, ""))
        {
            // Add headers
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for the response
            yield return request.SendWebRequest();

            // Handle the response
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}, Response Code: {request.responseCode}");
            }
            else if (request.responseCode == 200)
            {
                Debug.Log($"Success: {request.downloadHandler.text}");
            }
            else
            {
                Debug.LogError($"Unexpected response: {request.downloadHandler.text}, Response Code: {request.responseCode}");
            }
        }
    }

    IEnumerator UpdateUserDepartment(string newDepartment)
    {
        int userId = PlayerPrefs.GetInt("userID");
        Debug.Log($"User ID: {userId}, New Team: {newDepartment}");

        // Base URL of the API endpoint
        string baseUrl = $"https://phishfindersrealforrealsbs.org/api/v1/leaderboard/individual/department/{userId}?new_department={newDepartment}";

        // Create the UnityWebRequest for a PUT request
        using (UnityWebRequest request = UnityWebRequest.Put(baseUrl, ""))
        {
            // Add headers
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for the response
            yield return request.SendWebRequest();

            // Handle the response
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}, Response Code: {request.responseCode}");
            }
            else if (request.responseCode == 200)
            {
                Debug.Log($"Success: {request.downloadHandler.text}");
            }
            else
            {
                Debug.LogError($"Unexpected response: {request.downloadHandler.text}, Response Code: {request.responseCode}");
            }
        }
    }
}