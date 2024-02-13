using BudgetIntelligence2024.Persistence.DBContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Data;

namespace BudgetIntelligence2024.Persistence.StoredProcedures
{
    public class SpAddDistinctToTransactions : IAddDistinctToTransactions
    {
        private readonly string _name = "[dbo].[sp_AddDistinctToTransactions]";
        private readonly string _userIdParam = "UserId";
        private readonly string _rowCountOutputParam = "RowCount";

        private readonly BudgetIntelligenceDbContext _ctx;
        //private readonly ILogger _logger;

        public SpAddDistinctToTransactions(BudgetIntelligenceDbContext ctx) //, ILogger logger)
        {
            _ctx = ctx;
            //_logger = logger;
        }

        internal void Create(MigrationBuilder migrationBuilder)
        {
            var sp = $@"-- Inserts all new records from TransactionStaging to Transaction for a User
-- ie if it already exists in Transaction table then it will be ignored
-- Used to maintain the data integrity of the Transaction table

CREATE PROCEDURE {_name}
	@{_userIdParam} uniqueidentifier,
	@{_rowCountOutputParam} int Output	-- returns the number of rows inserted
AS
BEGIN

INSERT INTO [dbo].[Transaction]
           ([Id]
           ,[UserId]
		   ,[DateLocal]
           ,[Amount]
           ,[Type]
           ,[Description]
           ,[Balance]
		   ,[AccountId]
		   ,[ImportOrder]
           ,[IsDuplicate])
(
	select [Id]
            ,[UserId]
		   ,[DateLocal]
           ,[Amount]
           ,[Type]
           ,[Description]
           ,[Balance]
		   ,[AccountId]
		   ,[ImportOrder]
           ,0
	from [dbo].[TransactionStaging] AS ts
	WHERE UserId = @UserId
		AND NOT EXISTS
		(
			 Select 1
			 From [Transaction] AS t
			 Where ts.DateLocal = t.DateLocal
				 AND ts.Amount = t.Amount
				 AND ts.Description = t.Description
				 AND ts.UserId = t.UserId
		)
);

-- Return INSERT (only) row count, must be done before DELETE
Select @RowCount = @@ROWCOUNT;

-- Clear the staging table for the User
DELETE FROM [dbo].[TransactionStaging]
WHERE UserId = @UserId;

END

GO

";

            migrationBuilder.Sql(sp);
        }

        internal void Drop(MigrationBuilder migrationBuilder)
        {
            var sp = $@"DROP PROCEDURE {_name};
GO";

            migrationBuilder.Sql(sp);
        }

        public async Task<int> Execute(int userId)
        {
            try
            {
                int rowCount = 0;
                var userIdParam = new SqlParameter(_userIdParam, userId);

                var rowCountParam = new SqlParameter();
                rowCountParam.ParameterName = _rowCountOutputParam;
                rowCountParam.SqlDbType = SqlDbType.Int;
                rowCountParam.Direction = ParameterDirection.Output;

                await _ctx.Database.ExecuteSqlRawAsync("EXEC " + _name + " @UserId, @RowCount output", new[] { userIdParam, rowCountParam });
                rowCount = Convert.ToInt32(rowCountParam.Value);

                //_logger.Information($"{nameof(SpAddDistinctToTransactions)}: {rowCount} unique transactions added");

                return rowCount;
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, $"{nameof(SpAddDistinctToTransactions)} adding transactions failed");

                return -1;
            }
        }
    }
}