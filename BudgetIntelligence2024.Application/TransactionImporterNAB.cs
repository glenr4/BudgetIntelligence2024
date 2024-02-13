using BudgetIntelligence2024.Persistence;
using Microsoft.Extensions.Logging;

namespace BudgetIntelligence2024.Application;

public class TransactionImporterNAB : ITransactionImporter
{
    public TransactionImporterNAB()
    {
    }

    /// <summary>
    /// Imports transactions from a CSV file and inserts it into the TransactionStaging table
    /// Use of this method must be inside a try, catch statement
    /// </summary>
    /// <param name="fileName">The CSV filename</param>
    /// <returns>Status message</returns>
    ///
    //TODO: data validation, how do we know if we have received garbage? Maybe the wrong file was selected?
    public int ImportCSV(Stream file, int userId, int accountId)
    {
        string line = "";
        int lineCount = 0;
        List<ImportedTransactionDto> importedTransactions = new();

        //Logger.Debug(String.Format("ImportCSV: Importing {0} for userId: {1}, accountId: {2}", fileName, userId, accountId));

        // Process file line by line
        try
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string[] data;
                while ((line = sr.ReadLine()) != null)
                {
                    // Replace ",," with "," "," so that the split always creates the same number of array elements
                    line = line.Replace(@""",,""", @""",""Empty"",""");
                    line = line.Replace(@""",,,""", @""",""Empty"",""Empty"",""");

                    // Split into comma separated components
                    string[] separator = new string[] { "," };   //{ @""",""" }; //, @""",,""" }; // ","
                    data = line.Split(separator, StringSplitOptions.None);

                    // Add to DB
                    if (data.Length == 7)
                    {   //6) {
                        // Ignore first line which contains headings
                        if (data[0] == "\"Date")
                        {
                            continue;
                        }

                        // Remove remaining quotes and dollar signs
                        for (int i = 0; i < data.Length; i++)
                        {
                            data[i] = data[i].Replace(@"""", "");
                            data[i] = data[i].Replace("$", "");
                        }

                        // Array order is specific to NAB bank files
                        importedTransactions.Add(
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
                    }
                    else
                    {
                        //Logger.Debug(String.Format("ImportCSV: this line was not in the correct format: {0}", line));
                    }

                    lineCount++;
                }
            }

            return lineCount;

            // Save
            //this.Context.SaveChanges();

            //// Execute stored procedure to copy distinct entries added above to the staging table to the actual Transaction table
            ////TODO: change staging table to a temporary table created by the stored procedure to avoid contention.
            ////TODO: how do we avoid contention on the Transaction table, should there be one per user?
            //ObjectParameter rowCount = new ObjectParameter("RowCount", typeof(int));
            //this.Context.AddDistinctToTransactions(rowCount);   // TODO: need to prefix this stored procedure with "sp_"

            //Logger.Debug(String.Format("ImportCSV: contains {0} transactions, {1} were new", lineCount, Convert.ToString(rowCount.Value)));

            //return Convert.ToInt32(rowCount.Value);
        }
        catch (Exception ex)
        {
            string message = String.Format("ImportCSV: exception: {0}; lineCount: {1} line: {2}", ex.Message, lineCount, line);
            //Logger.Debug(message);

            throw new Exception(message, ex);
        }
    }
}
