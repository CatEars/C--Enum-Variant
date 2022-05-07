// See https://aka.ms/new-console-template for more information

using SessionResultTest;

var result = ApiCaller.Authenticate();

void OnAuthenticationError(SessionResult.AuthenticationError authError) 
{
    Console.WriteLine($"Failed to authenticate against '{authError.Url}' with user '{authError.User}'");
}

void OnCommunicationError(SessionResult.CommunicationError commError)
{
    Console.WriteLine($"Failed to reach {commError.Url} for authentication");
}

void OnConfigurationError(SessionResult.ConfigurationError confError)
{
    Console.WriteLine("Failed to authenticate because of configuration issues:");
    foreach (var issue in confError.Issues)
    {
        Console.WriteLine($"  - {issue}");
    }
}

if (result.Failed())
{
    Action failureAction = result switch
    {
        SessionResult.AuthenticationError authError => () => OnAuthenticationError(authError),
        SessionResult.CommunicationError commError => () => OnCommunicationError(commError),
        SessionResult.ConfigurationError confError => () => OnConfigurationError(confError),
        _ => () => throw new Exception("Unexpected failure type")
    };
    failureAction();
}
else
{
    var ticket = result.Unwrap().SessionKey;
    Console.WriteLine("Session ticket: " + ticket);
}
