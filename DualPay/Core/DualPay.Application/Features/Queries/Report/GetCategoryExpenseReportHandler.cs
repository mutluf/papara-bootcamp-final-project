using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs.Reports;
using MediatR;

namespace DualPay.Application.Features.Queries.Report;

public class GetCategoryExpenseReportHandler:IRequestHandler<GetCategoryExpenseReportQueryRequest,ApiResponse<List<GetCategoryExpenseReportQueryResponse>>>
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;
    
    public GetCategoryExpenseReportHandler(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<GetCategoryExpenseReportQueryResponse>>> Handle(GetCategoryExpenseReportQueryRequest request, CancellationToken cancellationToken)
    {
        List<CategoryExpenseReportDto> reports = await _reportService.GetCategoryExpenseReportAsync(request.StartDate, request.EndDate);
        var responses = _mapper.Map<List<GetCategoryExpenseReportQueryResponse>>(reports);
        
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