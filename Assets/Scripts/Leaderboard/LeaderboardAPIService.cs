using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LeaderboardAPIService
{
    private const string API_BASE_URL = "https://phishfindersrealforrealsbs.org/api/v1/leaderboard";

    public List<IndividualLeaderboardEntry> IndividualLeaderboard { get; private set; }
    public List<TeamLeaderboardEntry> TeamLeaderboard { get; private set; }
    public List<DepartmentLeaderboardEntry> DepartmentLeaderboard { get; private set; }

    public IEnumerator GetTopIndividuals(int limit = 10)
    {
        string url = $"{API_BASE_URL}/individual/top?limit={limit}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                IndividualLeaderboard = JsonConvert.DeserializeObject<List<IndividualLeaderboardEntry>>(json);
            }
        }
    }

    public IEnumerator GetTopTeams(int limit = 10)
    {
        string url = $"{API_BASE_URL}/team/top?limit={limit}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                TeamLeaderboard = JsonConvert.DeserializeObject<List<TeamLeaderboardEntry>>(json);
            }
        }
    }

    public IEnumerator GetTopDepartments(int limit = 10)
    {
        string url = $"{API_BASE_URL}/department/top?limit={limit}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                DepartmentLeaderboard = JsonConvert.DeserializeObject<List<DepartmentLeaderboardEntry>>(json);
            }
        }
    }

    // This method will fetch the user's team and department by email
    public IEnumerator GetUserTeamAndDepartment()
    {
        string userEmail = PlayerPrefs.GetString("Email");
        string url = $"https://phishfindersrealforrealsbs.org/api/v1/users/email/{userEmail}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Parse the response data
                string json = request.downloadHandler.text;
                UserInLeaderboardDB user = JsonConvert.DeserializeObject<UserInLeaderboardDB>(json);

                // Handle the retrieved team and department
                if (user != null)
                {
                    Debug.Log($"User {user.user_email} is part of team: {user.team_name} and department: {user.department}");
                    // You can now use user.team and user.department as needed
                    PlayerPrefs.SetString("Team", user.team_name);
                    PlayerPrefs.SetString("Department", user.department);
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
}