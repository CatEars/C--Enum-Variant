using System.Collections.Immutable;

namespace SessionResultTest;

// Implementation based on: https://spencerfarley.com/2021/03/26/unions-in-csharp/
public record SessionResult
{

    public record ConfigurationError(ImmutableList<string> Issues) : SessionResult;

    public record CommunicationError(string Url) : SessionResult;

    public record AuthenticationError(string Url, string User) : SessionResult;

    public record AuthenticatedSession(string SessionKey) : SessionResult;

    public bool IsOk()
    {
        return this switch
        {
            AuthenticatedSession => true,
            _ => false
        };
    }

    public bool Failed() => !IsOk();

    public AuthenticatedSession Unwrap()
    {
        if (Failed())
        {
            throw new ArgumentException();
        }

        return (AuthenticatedSession)this;
    }
    
    private SessionResult() {}
}