using Microsoft.EntityFrameworkCore.Migrations;


//I TRIED BUT I COULDN'T GET IT TO WORK
namespace DualPay.Persistence.StoredProceduresMigration
{
    public partial class AddStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_GetCategoryExpenseReport' AND type = 'P')
                BEGIN
                    CREATE PROCEDURE [dbo].[sp_GetCategoryExpenseReport]
                        @StartDate DATETIME,
                        @EndDate DATETIME
                    AS
                    BEGIN
                        SELECT 
                            c.Name as Category,
                            SUM(Amount) AS TotalAmount,
                            COUNT(*) AS TotalTransactions,
                            AVG(Amount) AS AverageSpending
                        FROM Expenses
                        INNER JOIN ExpenseCategories c on Expenses.ExpenseCategoryId = c.Id
                        WHERE Status = 'Paid'
                            AND ApprovedDate BETWEEN @StartDate AND @EndDate
                        GROUP BY c.Name
                        ORDER BY TotalAmount DESC
                    END
                END
            ");
            
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_GetEmployeeExpenseHistory' AND type = 'P')
                BEGIN
                    CREATE PROCEDURE [dbo].[sp_GetEmployeeExpenseHistory]
                        @EmployeeId INT
                    AS
                    BEGIN
                        SELECT 
                            e.EmployeeId,
                            e.Id AS RequestId,
                            FORMAT(e.CreatedAt, 'yyyy-MM-dd HH:mm') as CreatedDate,
                            e.Amount,
                            e.Status,
                            c.Name as Category
                        FROM Expenses e
                        INNER JOIN ExpenseCategories c ON e.ExpenseCategoryId = c.Id
                        WHERE e.EmployeeId = @EmployeeId
                        ORDER BY e.CreatedAt DESC
                    END
                END
            ");
            
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_GetEmployeeSpendingsSummary' AND type = 'P')
                BEGIN
                    CREATE PROCEDURE [dbo].[sp_GetEmployeeSpendingsSummary]
                        @StartDate DATETIME,
                        @EndDate DATETIME
                    AS
                    BEGIN
                        SELECT 
                            EmployeeId,
                            u.Name,
                            SUM(Amount) AS TotalAmount,
                            COUNT(*) AS TotalTransactions,
                            AVG(Amount) AS AverageSpending
                        FROM Expenses
                        INNER JOIN Employees e on Expenses.EmployeeId = Expenses.EmployeeId
                        INNER JOIN AspNetUsers u on e.UserId = u.Id
                        WHERE Status = 'Paid'
                            AND ApprovedDate BETWEEN @StartDate AND @EndDate
                        GROUP BY EmployeeId, Name
                        ORDER BY TotalAmount DESC
                    END
                END
            ");
            
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_GetPayments' AND type = 'P')
                BEGIN
                    CREATE PROCEDURE [dbo].[sp_GetPayments]
                        @StartDate DATE,
                        @EndDate DATE
                    AS
                    BEGIN
                        SELECT       
                            SUM(Amount) AS TotalAmount
                        FROM Expenses
                        WHERE Status = 'Paid' AND ApprovedDate BETWEEN @StartDate AND @EndDate
                    END
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_GetCategoryExpenseReport' AND type = 'P') DROP PROCEDURE dbo.sp_GetCategoryExpenseReport");
            migrationBuilder.Sql("IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_GetEmployeeExpenseHistory' AND type = 'P') DROP PROCEDURE dbo.sp_GetEmployeeExpenseHistory");
            migrationBuilder.Sql("IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_GetEmployeeSpendingsSummary' AND type = 'P') DROP PROCEDURE dbo.sp_GetEmployeeSpendingsSummary");
            migrationBuilder.Sql("IF EXISTS (SELECT * FROM sysobjects WHERE name = 'sp_GetPayments' AND type = 'P') DROP PROCEDURE dbo.sp_GetPayments");
        }
    }
}
