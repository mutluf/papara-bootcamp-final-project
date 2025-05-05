using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs.Reports;
using MediatR;

namespace DualPay.Application.Features.Queries.Report;

public class GetEmployeeExpenseReportQueryHandler 
    :IRequestHandler<GetEmployeeExpenseReportQueryRequest,
    ApiResponse<List<GetEmployeeExpenseReportQueryResponse>>>
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;
    public GetEmployeeExpenseReportQueryHandler(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<GetEmployeeExpenseReportQueryResponse>>> Handle(GetEmployeeExpenseReportQueryRequest request, 
        CancellationToken cancellationToken)
    {
        List<EmployeeExpenseReportDto> reports = await _reportService.GetEmployeeExpenseHistoryReportAsync(request.EmployeeId);
        var responses = _mapper.Map<List<GetEmployeeExpenseReportQueryResponse>>(reports);
        
        return new ApiResponse<List<GetEmployeeExpenseReportQueryResponse>>(responses);
    }
}

public class GetEmployeeExpenseReportQueryRequest: IRequest<ApiResponse<List<GetEmployeeExpenseReportQueryResponse>>>
{
    public int EmployeeId { get; set; }
}

public class GetEmployeeExpenseReportQueryResponse
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int ExpenseId { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string CreatedDate { get; set; }
}