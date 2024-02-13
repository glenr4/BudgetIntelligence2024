namespace BudgetIntelligence2024.Application;

public class TransactionParserNAB : ITransactionParser
{
    //TODO: data validation, how do we know if we have received garbage? Maybe the wrong file was selected?
    public IEnumerable<ImportedTransactionDto> ParseCSV(Stream file, int userId, int accountId)
    {
        string line = "";
        int lineCount = 0;
        List<ImportedTransactionDto> transactions = new();

        //Logger.Debug(String.Format("ImportCSV: Importing {0} for userId: {1}, accountId: {2}", fileName, userId, accountId));

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

                    // Array order is specific to NAB bank files
                    transactions.Add(
                        new ImportedTransactionDto()
                        {
                            Date = DateOnly.Parse(data[0]), // new CultureInfo("en-AU"), style: DateTimeStyles.AssumeLocal),
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
            string message = String.Format("ImportCSV: exception: {0}; lineCount: {1} line: {2}", ex.Message, lineCount, line);
            //Logger.Debug(message);

            throw new Exception(message, ex);
        }
    }

    private void LogFormatError(string line)
    {
        //Logger.Debug(String.Format("ImportCSV: this line was not in the correct format: {0}", line));
    }
}
