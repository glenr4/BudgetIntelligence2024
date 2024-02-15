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
    public IEnumerable<ImportedTransactionDto> ParseCSV(string fileName, Stream file, int userId, int accountId)
    {
        int lineCount = 0;
        
        try
        {
            List<ImportedTransactionDto> transactions = new();
        
            _logger.LogInformation($"'{nameof(TransactionParserNAB)}': Importing '{fileName}' for userId: {userId}, accountId: {accountId}");

            using (StreamReader sr = new StreamReader(file))
            {
                string[] data;
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    string[] separator = new string[] { "," };
                    data = line.Split(separator, StringSplitOptions.None);

                    if (data.Length != 9)
                    {
                        LogFormatError(lineCount);

                        continue;
                    }

                    if (data[0] == "Date")
                    {
                        LogFormatError(lineCount);

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
            throw new Exception("testing");

            return transactions;
        }
        catch (Exception ex)
        {
            string message = $"'{nameof(TransactionParserNAB)}': could not parse line: {lineCount}";

            //_logger.LogError(message);

            throw new CSVParseException(message, ex);
        }
    }

    private void LogFormatError(int lineCount)
    {
        _logger.LogWarning($"'{nameof(TransactionParserNAB)}': line {lineCount} was not in the correct format");
    }
}
