using System.Collections.Immutable;

namespace SessionResultTest;

public class ApiCaller
{

    public static SessionResult Authenticate()
    {
        var random = new Random();
        var diceRoll = random.Next(0, 4);
        if (diceRoll == 0)
        {
            var issues = ImmutableList<string>.Empty
                .Add("Configuration was missing");
            return new SessionResult.ConfigurationError(issues);
        } 
        else if (diceRoll == 1)
        {
            return new SessionResult.CommunicationError("http://example.com");
        } 
        else if (diceRoll == 2)
        {
            return new SessionResult.AuthenticationError("http://example.com", "User");
        } 
        else if (diceRoll == 3)
        {
            var ticket = Guid.NewGuid().ToString();
            return new SessionResult.AuthenticatedSession(ticket);
        }
        else
        {
            throw new Exception();
        }
    }
    
}