using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;
using MediatR;
namespace DualPay.Application.Features.Commands.ExpenseCategories;

public class UpdateExpenseCommandHandler: IRequestHandler<UpdateExpenseCommandRequest, ApiResponse>
{
    private readonly IExpenseService  _expenseService;
    private readonly IMapper _mapper;

    public UpdateExpenseCommandHandler(IMapper mapper, IExpenseService expenseService)
    {
        _mapper = mapper;
        _expenseService = expenseService;
    }

    public async Task<ApiResponse> Handle(UpdateExpenseCommandRequest request, CancellationToken cancellationToken)
    {
        ExpenseDto dto = await _expenseService.GetByIdAsync(request.Id);
        if (dto == null)
            return new ApiResponse("Expense not found");

        ExpenseDto expenseDto = _mapper.Map<ExpenseDto>(request);
        _expenseService.UpdateAsync(expenseDto);
        return new ApiResponse();
    }
}

public class UpdateExpenseCommandRequest: IRequest<ApiResponse>
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public int? ExpenseCategoryId { get; set; }
    public decimal? Amount { get; set; }
    public string? DocumentUrl { get; set; }
    public string? Location { get; set; }
}