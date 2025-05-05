using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs.Reports;
using MediatR;

namespace DualPay.Application.Features.Queries.Report;

public class GetEmployeeSpendingsQueryHandler 
    :IRequestHandler<GetEmployeeSpendingReportQueryRequest,
        ApiResponse<List<GetEmployeeSpendingReportQueryResponse>>>
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    public GetEmployeeSpendingsQueryHandler(IReportService reportService, IMapper mapper, ICacheService cacheService)
    {
        _reportService = reportService;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<ApiResponse<List<GetEmployeeSpendingReportQueryResponse>>> Handle(GetEmployeeSpendingReportQueryRequest request, 
        CancellationToken cancellationToken)
    {
        var cacheKey = $"EmployeeSpendings_{request.StartDate:yyyyMMdd}_{request.EndDate:yyyyMMdd}";
        var cached = await _cacheService.GetAsync<List<GetEmployeeSpendingReportQueryResponse>>(cacheKey);
        if (cached is not null)
            return new ApiResponse<List<GetEmployeeSpendingReportQueryResponse>>(cached);

        List<EmployeeSpendingReportDto> reports = await _reportService.GetEmployeeSpendingsReportAsync(request.StartDate, request.EndDate);
        var responses = _mapper.Map<List<GetEmployeeSpendingReportQueryResponse>>(reports);
        await _cacheService.SetAsync(cacheKey, responses, TimeSpan.FromHours(1));
        
        return new ApiResponse<List<GetEmployeeSpendingReportQueryResponse>>(responses);
    }
}

public class GetEmployeeSpendingReportQueryRequest: IRequest<ApiResponse<List<GetEmployeeSpendingReportQueryResponse>>>
{
    public DateTime StartDate { get; set; } 
    public DateTime EndDate  { get; set; }
}

public class GetEmployeeSpendingReportQueryResponse
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalTransactions{ get; set; }
    public decimal AverageSpending{ get; set; }
}