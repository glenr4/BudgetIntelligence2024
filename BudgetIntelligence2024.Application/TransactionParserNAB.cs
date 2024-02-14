using Microsoft.Extensions.Logging;

namespace BudgetIntelligence2024.Application;

public class TransactionParserNAB : ITransactionParser
{
    private readonly ILogger _logger;

    public TransactionParserNAB(ILogger<TransactionParserNAB> logger)
    {
        _logger = logger;
    }

    //TODO: data validation, how do we know if we have received garbage? Maybe the wrong file was selected?
    public IEnumerable<ImportedTransactionDto> ParseCSV(Stream file, int userId, int accountId)
    {
        // TODO
        string fileName = "How can we get the file name?";



        string line = "";
        int lineCount = 0;
        List<ImportedTransactionDto> transactions = new();

        _logger.LogDebug($"'{nameof(TransactionParserNAB)}': Importing {fileName} for userId: {userId}, accountId: {accountId}");

        try
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string[] data;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] separator = new string[] { "," };
                    data = line.Split(separator, StringSplitOptions.None);

                    if (data.Length != 9)
                    {
                        LogFormatError(line);

                        continue;
                    }

                    if (data[0] == "Date")
                    {
                        LogFormatError(line);

                        continue;
                    }

                    transactions.Add(
                        new ImportedTransactionDto()
                        {
                            Date = DateOnly.Parse(data[0]),
                            Amount = Convert.ToDecimal(data[1]),
                            Type = data[4],
                            Description = data[5],
                            Balance = Convert.ToDecimal(data[6]),
                            AccountId = accountId,
                            UserId = userId
                        }
                    );

                    lineCount++;
                }
            }

            return transactions;
        }
        catch (Exception ex)
        {
            string message = $"'{nameof(TransactionParserNAB)}': exception: {ex.Message}; lineCount: {lineCount} line: {line}";
            _logger.LogDebug(message);

            throw new Exception(message, ex);
        }
    }

    /// <summary>
    /// Note: this logs sensitive information
    /// </summary>
    /// <param name="line"></param>
    private void LogFormatError(string line)
    {
        _logger.LogDebug($"'{nameof(TransactionParserNAB)}': this line was not in the correct format: {line}");
    }
}
