using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs.Reports;
using MediatR;

namespace DualPay.Application.Features.Queries.Report;

public class GetCategoryExpenseReportHandler:IRequestHandler<GetCategoryExpenseReportQueryRequest,ApiResponse<List<GetCategoryExpenseReportQueryResponse>>>
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    public GetCategoryExpenseReportHandler(IReportService reportService, IMapper mapper, ICacheService cacheService)
    {
        _reportService = reportService;
        _mapper = mapper;
        _cacheService = cacheService;
    }
    public async Task<ApiResponse<List<GetCategoryExpenseReportQueryResponse>>> Handle(GetCategoryExpenseReportQueryRequest request, CancellationToken cancellationToken)
    {
        var cacheKey = $"CategoryExpenseReports_{request.StartDate:yyyyMMdd}_{request.EndDate:yyyyMMdd}";
        var cached = await _cacheService.GetAsync<List<GetCategoryExpenseReportQueryResponse>>(cacheKey);
        if (cached is not null)
            return new ApiResponse<List<GetCategoryExpenseReportQueryResponse>>(cached);
        
        List<CategoryExpenseReportDto> reports = await _reportService.GetCategoryExpenseReportAsync(request.StartDate, request.EndDate);
        var responses = _mapper.Map<List<GetCategoryExpenseReportQueryResponse>>(reports);
        await _cacheService.SetAsync(cacheKey, responses, TimeSpan.FromMinutes(15));
        
        return new ApiResponse<List<GetCategoryExpenseReportQueryResponse>>(responses);
    }
}

public class GetCategoryExpenseReportQueryRequest : IRequest<ApiResponse<List<GetCategoryExpenseReportQueryResponse>>>
{
    public DateTime StartDate { get; set; } 
    public DateTime EndDate  { get; set; }
}
public class GetCategoryExpenseReportQueryResponse
{
    public string Category { get; set; }
    public string AverageSpending { get; set; }
    public int TotalAmount { get; set; }
    public int TotalTransactions { get; set; }
}