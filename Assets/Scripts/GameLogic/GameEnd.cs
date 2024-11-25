using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using TMPro;

public class GameEnd : MonoBehaviour
{
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI scoreText;

    void OnEnable()
    {
        Debug.Log("Here");
        waveNumberText.text = ((EndlessWS.waveNumber)/3).ToString() + " Waves Cleared!";
        scoreText.text = "Score: " + (PlayerInfo.EndlessScore).ToString();

        StartCoroutine(HandleOnEnable());
    }

    IEnumerator HandleOnEnable()
    {
        string userEmail = PlayerPrefs.GetString("Email");

        // Check if a current record for the player exists
        yield return StartCoroutine(CheckIfUserExists(userEmail));

        // Check if there isw a valid user
        int userId = PlayerPrefs.GetInt("UserID", -1);

        // If there is a user, update the user score
        if (userId != -1)
        {
            Debug.Log($"User found: {userId}");
            // Update the player score based on User ID
            yield return StartCoroutine(UpdateUserScore(userId, PlayerPrefs.GetInt("HighScore", 0)));

        }
        else // There is no existing record in the leaderboard, create a new user with their current score
        {
            Debug.Log("User not found");
            yield return StartCoroutine(CreateNewUser(userEmail, PlayerPrefs.GetInt("HighScore", 0)));
        }
    }

    // Coroutine that checks if the user exists
    IEnumerator CheckIfUserExists(string email)
    {
        string BaseUrl = "https://phishfindersrealforrealsbs.org/api/v1/users/email/";
        string url = BaseUrl + UnityWebRequest.EscapeURL(email);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}");
                yield return null;  // Return null if error occurs
            }
            else
            {
                if (request.responseCode == 404)
                {
                    Debug.Log("User not found.");
                    yield return null;  // Return null if user not found
                }
                else if (request.responseCode == 200)
                {
                    // Deserialize the JSON response into a UserInLeaderboardDB object
                    UserInLeaderboardDB user = JsonUtility.FromJson<UserInLeaderboardDB>(request.downloadHandler.text);

                    // Save the UserID
                    PlayerPrefs.SetInt("UserID", user.user_id);
                    yield return user; // Return the user object if found
                }
            }
        }
    }

    IEnumerator CreateNewUser(string email, int score)
    {
        // Prepare the user creation data
        string jsonData = JsonUtility.ToJson(new
        {
            user_email = email,
            endless_score = score,
            team_name = "DefaultTeam",  // Set other default values
            department = "DefaultDepartment"
        });

        string url = "https://phishfindersrealforrealsbs.org/api/v1/users/";

        // Create the POST request
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, jsonData))
        {
            // Set the content type
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the POST request
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                // Parse the response if user is created successfully
                if (request.responseCode == 200)
                {
                    Debug.Log("User created successfully: " + request.downloadHandler.text);
                }
                else
                {
                    Debug.LogError("Failed to create user. Response: " + request.downloadHandler.text);
                }
            }
        }
    }

    IEnumerator UpdateUserScore(int userId, int newScore)
    {
        // Base URL of the API endpoint
        string baseUrl = $"https://phishfindersrealforrealsbs.org/api/v1/leaderboard/individual/score/{userId}";

        // Add query parameter
        string urlWithQuery = $"{baseUrl}?new_score={newScore}";

        // Create the UnityWebRequest for a PUT request
        using (UnityWebRequest request = UnityWebRequest.Put(urlWithQuery, ""))
        {
            // Optional: Add headers if needed
            request.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for the response
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}");
                Debug.LogError($"Response Code: {request.responseCode}");
            }
            else
            {
                // Handle success
                Debug.Log($"Success: {request.downloadHandler.text}");
            }
        }
    }
}