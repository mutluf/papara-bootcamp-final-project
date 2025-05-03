using System.Data;
using Dapper;
using DualPay.Application.Abstraction;
using DualPay.Application.DTOs.Reports;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DualPay.Persistence.Repositories;

public class ReportRepository: IReportRepository
{
    private readonly IDbConnection _dbConnection;

    public ReportRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public async Task<List<DailyExpenseReportDto>> GetDailyExpenseReportAsync()
    {
        var sql = "SELECT * FROM vw_DailyExpenseReport";
        var result = await _dbConnection.QueryAsync<DailyExpenseReportDto>(sql);
        return result.ToList();
    }
}