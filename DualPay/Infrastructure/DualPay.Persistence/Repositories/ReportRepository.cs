using System.Data;
using Dapper;
using DualPay.Application.Abstraction;
using DualPay.Application.DTOs.Reports;

namespace DualPay.Persistence.Repositories;

public class ReportRepository: IReportRepository
{
    private readonly IDbConnection _dbConnection;

    public ReportRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public async Task<List<PaymentReportDto>> GetPaymentsReportAsync(DateTime startDate, DateTime endDate)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@StartDate", startDate);
        parameters.Add("@EndDate", endDate);
    
        var result = await _dbConnection.QueryAsync<PaymentReportDto>(
            "sp_GetPayments", parameters, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }

    public async Task<List<EmployeeExpenseReportDto>> GetEmployeeExpenseHistoryReportAsync(int employeeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", employeeId);

        var result = await _dbConnection.QueryAsync<EmployeeExpenseReportDto>(
            "sp_GetEmployeeExpenseHistory", parameters, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }

    public async Task<List<EmployeeSpendingReportDto>> GetEmployeeSpendingsReportAsync(DateTime startDate, DateTime endDate)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@StartDate", startDate);
        parameters.Add("@EndDate", endDate);
    
        var result = await _dbConnection.QueryAsync<EmployeeSpendingReportDto>(
            "sp_GetEmployeeSpendingsSummary", parameters, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }
    
    public async Task<List<CategoryExpenseReportDto>> GetCategoryExpenseReportAsync(DateTime startDate, DateTime endDate)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@StartDate", startDate);
        parameters.Add("@EndDate", endDate);
    
        var result = await _dbConnection.QueryAsync<CategoryExpenseReportDto>(
            "sp_GetCategoryExpenseReport", parameters, commandType: CommandType.StoredProcedure);
        return result.ToList();
    }
}