USE [DualPayDb]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCategoryExpenseReport]    Script Date: 5.05.2025 20:52:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[sp_GetEmployeeExpenseHistory]    Script Date: 5.05.2025 20:52:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[sp_GetEmployeeSpendingsSummary]    Script Date: 5.05.2025 20:52:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPayments]    Script Date: 5.05.2025 20:52:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
GO
