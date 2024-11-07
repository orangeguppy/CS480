using System;

[Serializable]
public class EmailData
{
    public string from;
    public string to;
    public string subject;
    public string content;
    public bool is_modified;
}

[Serializable]
public class EmailResponse
{
    public EmailJson email;
    public bool is_modified;
}

[Serializable]
public class EmailJson
{
    public string from;
    public string to;
    public string subject;
    public string content;
}

public static class EmailExtensions
{
    public static EmailData ToEmailData(this EmailResponse response)
    {
        return new EmailData
        {
            from = response.email.from,
            to = response.email.to,
            subject = response.email.subject,
            content = response.email.content,
            is_modified = response.is_modified
        };
    }
}
