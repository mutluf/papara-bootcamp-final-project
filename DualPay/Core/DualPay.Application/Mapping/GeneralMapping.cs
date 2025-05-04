using AutoMapper;
using DualPay.Application.DTOs;
using DualPay.Application.Features.Commands;
using DualPay.Application.Features.Commands.ExpenseCategories;
using DualPay.Application.Features.Queries;
using DualPay.Domain.Entities;
using EmployeeResponse = DualPay.Application.Features.Queries.EmployeeResponse;
using ExpenseResponse = DualPay.Application.Features.Queries.ExpenseResponse;

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
        CreateMap<ExpenseDto, ExpenseDetailResponse>();
        CreateMap<ExpenseDto, Expense>().ReverseMap();
        
        CreateMap<CreateEmployeeCommandRequest, EmployeeDto>();
        CreateMap<EmployeeDto, EmployeeResponse>();
        CreateMap<EmployeeDto, EmployeeDetailResponse>();
        CreateMap<EmployeeDto, Employee>();
        CreateMap< Employee, EmployeeDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AppUser.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.AppUser.Surname));
    }
}