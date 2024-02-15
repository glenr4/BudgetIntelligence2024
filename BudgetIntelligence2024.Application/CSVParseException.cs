namespace BudgetIntelligence2024.Application;

public class CSVParseException : ApplicationException
{
    public CSVParseException(string? message) : base(message)
    {
    }

    public CSVParseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
