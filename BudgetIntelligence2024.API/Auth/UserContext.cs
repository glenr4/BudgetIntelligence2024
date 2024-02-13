namespace BudgetIntelligence2024.API.Auth;

public class UserContext : IUserContext
{
    /// <summary>
    /// TODO set this from the access token when Auth is implemented
    /// </summary>
    public int UserId { get; set; } = 1;
}
