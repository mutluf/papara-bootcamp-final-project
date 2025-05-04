using AutoMapper;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;
using MediatR;

namespace DualPay.Application.Features.Queries;

public class GetExpenseByIdQueryHandler: IRequestHandler<GetExpenseByIdRequest, ApiResponse<ExpenseDetailResponse>>
{
    private readonly IExpenseService _expenseService;
    private readonly IMapper _mapper;

    public GetExpenseByIdQueryHandler(IMapper mapper, IExpenseService expenseService)
    {
        _mapper = mapper;
        _expenseService = expenseService;
    }

    public async Task<ApiResponse<ExpenseDetailResponse>> Handle(GetExpenseByIdRequest request, CancellationToken cancellationToken)
    {
        ExpenseDto expenseDto = await _expenseService.GetByIdAsync(request.Id);
        ExpenseDetailResponse mapped = _mapper.Map<ExpenseDetailResponse>(expenseDto);
        return new ApiResponse<ExpenseDetailResponse>(mapped);
    }
}

public class GetExpenseByIdRequest : IRequest<ApiResponse<ExpenseDetailResponse>>
{
    public int Id { get; set; }
}

public class ExpenseDetailResponse
{
    public string Description { get; set; }
    public int ExpenseCategoryId { get; set; }
    public decimal Amount { get; set; }
    public ExpenseStatus Status { get; set; }
    public string? DocumentUrl { get; set; }
    public string Location { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public DateTime? RejectedDate { get; set; }
    public string? ApprovedBy { get; set; }
    public string? RejectedBy { get; set; }
    public string? RejectionReason { get; set; }
}