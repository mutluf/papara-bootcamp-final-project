using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs.Reports;
using MediatR;

namespace DualPay.Application.Features.Queries.Report;

public class GetPaymentsReportQueryHandler:IRequestHandler<GetPaymentsReportQueryRequest,ApiResponse<List<GetPaymentsReportQueryResponse>>>
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    public GetPaymentsReportQueryHandler(IReportService reportService, IMapper mapper, ICacheService cacheService)
    {
        _reportService = reportService;
        _mapper = mapper;
        _cacheService = cacheService;
    }
    public async Task<ApiResponse<List<GetPaymentsReportQueryResponse>>> Handle(GetPaymentsReportQueryRequest request, CancellationToken cancellationToken)
    {
        var cacheKey = $"PaymentReports_{request.StartDate:yyyyMMdd}_{request.EndDate:yyyyMMdd}";
        var cached = await _cacheService.GetAsync<List<GetPaymentsReportQueryResponse>>(cacheKey);
        if (cached is not null)
            return new ApiResponse<List<GetPaymentsReportQueryResponse>>(cached);
        
        List<PaymentReportDto> reports = await _reportService.GetPaymentsReportAsync(request.StartDate, request.EndDate);
        var responses = _mapper.Map<List<GetPaymentsReportQueryResponse>>(reports);
        await _cacheService.SetAsync(cacheKey, responses, TimeSpan.FromMinutes(15));
        
        return new ApiResponse<List<GetPaymentsReportQueryResponse>>(responses);
    }
}

public class GetPaymentsReportQueryRequest : IRequest<ApiResponse<List<GetPaymentsReportQueryResponse>>>
{
    /// <example>2025-01-01</example>
    public DateTime StartDate { get; set; } 
    /// <example>2025-01-10</example>
    public DateTime EndDate  { get; set; }
}
public class GetPaymentsReportQueryResponse
{
    public decimal TotalAmount { get; set; }
}