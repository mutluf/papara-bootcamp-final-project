using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs.Reports;
using MediatR;

namespace DualPay.Application.Features.Queries.Report;

public class GetPaymentsReportQueryHandler:IRequestHandler<GetPaymentsReportQueryRequest,ApiResponse<List<GetPaymentsReportQueryResponse>>>
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;
    
    public GetPaymentsReportQueryHandler(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<GetPaymentsReportQueryResponse>>> Handle(GetPaymentsReportQueryRequest request, CancellationToken cancellationToken)
    {
        List<PaymentReportDto> reports = await _reportService.GetPaymentsReportAsync(request.StartDate, request.EndDate);
        var responses = _mapper.Map<List<GetPaymentsReportQueryResponse>>(reports);
        
        return new ApiResponse<List<GetPaymentsReportQueryResponse>>(responses);
    }
}

public class GetPaymentsReportQueryRequest : IRequest<ApiResponse<List<GetPaymentsReportQueryResponse>>>
{
    public DateTime StartDate { get; set; } 
    public DateTime EndDate  { get; set; }
}
public class GetPaymentsReportQueryResponse
{
    public decimal TotalAmount { get; set; }
}