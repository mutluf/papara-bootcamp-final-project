using AutoMapper;

using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Application.Features.Commands.ExpenseCategories;
using DualPay.Domain.Entities;
using MediatR;

namespace DualPay.Application.Features.Queries;

public class GetExpenseCategoryByIdQueryHandler: IRequestHandler<GetExpenseCategoryByIdRequest, ApiResponse<ExpenseCategoryResponse>>
{
    private readonly IExpenseCategoryService _expenseCategoryService;
    private readonly IMapper _mapper;

    public GetExpenseCategoryByIdQueryHandler(IMapper mapper, IExpenseCategoryService expenseCategoryService)
    {
        _mapper = mapper;
        _expenseCategoryService = expenseCategoryService;
    }

    public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(GetExpenseCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        ExpenseCategoryDto expenseCategoryDto = await _expenseCategoryService.GetByIdAsync(request.Id);
        ExpenseCategoryResponse mapped = _mapper.Map<ExpenseCategoryResponse>(expenseCategoryDto);
        return new ApiResponse<ExpenseCategoryResponse>(mapped);
    }
}

public class GetExpenseCategoryByIdRequest : IRequest<ApiResponse<ExpenseCategoryResponse>>
{
    public int Id { get; set; }
}

