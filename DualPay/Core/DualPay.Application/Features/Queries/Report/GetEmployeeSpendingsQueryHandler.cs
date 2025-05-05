using AutoMapper;
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
    public GetEmployeeSpendingsQueryHandler(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GetEmployeeSpendingReportQueryResponse>>> Handle(GetEmployeeSpendingReportQueryRequest request, 
        CancellationToken cancellationToken)
    {
        List<EmployeeSpendingReportDto> reports = await _reportService.GetEmployeeSpendingsReportAsync(request.StartDate, request.EndDate);
        var responses = _mapper.Map<List<GetEmployeeSpendingReportQueryResponse>>(reports);
        
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