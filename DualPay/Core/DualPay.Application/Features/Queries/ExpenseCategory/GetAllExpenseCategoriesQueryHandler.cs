using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.DTOs;
using DualPay.Application.Features.Commands.ExpenseCategories;
using DualPay.Domain.Entities;
using MediatR;

namespace DualPay.Application.Features.Queries;

public class GetAllExpenseCategoriesQueryHandler : IRequestHandler<GetAllExpenseCategoriesRequest,ApiResponse<List<ExpenseCategoryResponse>>>
{
    private readonly IExpenseCategoryService _expenseCategoryService;
    private readonly IMapper _mapper;

    public GetAllExpenseCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IExpenseCategoryService expenseCategoryService)
    {
        _mapper = mapper;
        _expenseCategoryService = expenseCategoryService;
    }

    public async Task<ApiResponse<List<ExpenseCategoryResponse>>> Handle(GetAllExpenseCategoriesRequest request, CancellationToken cancellationToken)
    {
        List<ExpenseCategoryDto> expenseCategoryDtos = await _expenseCategoryService.GetAllAsync();
        List<ExpenseCategoryResponse> mapped = _mapper.Map<List<ExpenseCategoryResponse>>(expenseCategoryDtos);
        return new ApiResponse<List<ExpenseCategoryResponse>>(mapped);
    }
}

public class GetAllExpenseCategoriesRequest : IRequest<ApiResponse<List<ExpenseCategoryResponse>>>
{
    
}
