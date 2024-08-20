[System.Serializable]
public class LoginResponse
{
    public string access_token;
    public string token_type;
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
}

[System.Serializable]
public class PWResetRequest
{
    public string username;
    public int otp;
    public string password;
}