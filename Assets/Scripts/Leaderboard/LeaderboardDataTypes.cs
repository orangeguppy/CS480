[System.Serializable]
public class IndividualLeaderboardEntry
{
    public int rank;
    public string user_email;
    public int endless_score;
    public string team_name;
}

[System.Serializable]
public class TeamLeaderboardEntry
{
    public int rank;
    public string team_name;
    public int total_score;
}

[System.Serializable]
public class DepartmentLeaderboardEntry
{
    public int rank;
    public string department;
    public int total_score;
}