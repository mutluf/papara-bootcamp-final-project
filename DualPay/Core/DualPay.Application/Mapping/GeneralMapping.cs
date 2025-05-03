using AutoMapper;
using DualPay.Application.DTOs;
using DualPay.Application.Features.Commands;
using DualPay.Application.Features.Commands.ExpenseCategories;
using DualPay.Domain.Entities;

namespace DualPay.Application.Mapping;

public class GeneralMapping :Profile
{
    public GeneralMapping()
    {
        CreateMap<CreateExpenseCategoryCommandRequest, ExpenseCategoryDto>();
        CreateMap<ExpenseCategoryDto, ExpenseCategoryResponse>();
        CreateMap<ExpenseCategoryDto, ExpenseCategory>().ReverseMap();
        
        CreateMap<CreateExpenseCommandRequest, ExpenseDto>();
        CreateMap<ExpenseDto, ExpenseResponse>();
        CreateMap<ExpenseDto, Expense>().ReverseMap();
        
        CreateMap<CreateEmployeeCommandRequest, EmployeeDto>();
        CreateMap<EmployeeDto, EmployeeResponse>();
        CreateMap<EmployeeDto, Employee>().ReverseMap();
    }
}