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