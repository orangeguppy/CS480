using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class LeaderboardAPIService
{
    private const string API_BASE_URL = "http://localhost:8000/api/v1/leaderboard";

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
}