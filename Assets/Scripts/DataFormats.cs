using System;

[System.Serializable]
public class LoginResponse
{
    public string access_token;
    public string token_type;
    public string session_id;
    public SessionData session;
}

[System.Serializable]
public class UserData
{
    public string username;
    public string password;
    public string role;
}

[System.Serializable]
public class OTPRequest
{
    public string username;
    public bool new_acc;
}

[System.Serializable]
public class PWResetRequest
{
    public string username;
    public int otp;
    public string password;
}

[System.Serializable]
public class AccActivationRequest
{
    public string username;
    public int otp;
}

[System.Serializable]
public class HTTPResponse
{
    public string detail;
}

[Serializable]
public class SessionData
{
    public string session_id;
    public string username;
    public DateTime created_at;
    public DateTime expires_at;
}

[System.Serializable]
public class NewUserLeaderboard
{
    public string user_email;
    public string team_name;
    public string department;
}

[System.Serializable]
public class UserInLeaderboardDB
{
    public int user_id;
    public string user_email;
    public int social_engineering_score;
    public int bec_and_quishing_score;
    public int email_web_score;
    public int auth_score;
    public int ssrf_score;
    public int endless_score;
    public string team_name;
    public string department;
    public string last_updated;
}


[System.Serializable]
public class SessionID
{
    public string session_id;
}

[System.Serializable]
public class UpdateTeamRequest
{
    public string new_team;
}