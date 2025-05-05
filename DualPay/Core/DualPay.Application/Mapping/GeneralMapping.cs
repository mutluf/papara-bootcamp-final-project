using AutoMapper;
using DualPay.Application.DTOs;
using DualPay.Application.DTOs.Reports;
using DualPay.Application.Features.Commands;
using DualPay.Application.Features.Commands.ExpenseCategories;
using DualPay.Application.Features.Queries;
using DualPay.Application.Features.Queries.Report;
using DualPay.Domain.Entities;

namespace DualPay.Application.Mapping;
public class GeneralMapping :Profile
{
    public GeneralMapping()
    {
        CreateMap<CreateExpenseCategoryCommandRequest, ExpenseCategoryDto>();
        CreateMap<UpdateExpenseCategoryCommandRequest, ExpenseCategoryDto>();
        CreateMap<ExpenseCategoryDto, ExpenseCategoryResponse>();
        CreateMap<ExpenseCategoryDto, ExpenseCategory>().ReverseMap();
        
        CreateMap<CreateExpenseCommandRequest, ExpenseDto>();
        CreateMap<UpdateExpenseCommandRequest, ExpenseDto>();
        CreateMap<ExpenseDto, ExpenseResponse>().ReverseMap();
        CreateMap<ExpenseDto, ExpenseDetailResponse>();
        CreateMap<ExpenseDto, Expense>().ReverseMap();
        
        CreateMap<CreateEmployeeCommandRequest, EmployeeDto>();
        CreateMap<UpdateEmployeeCommandRequest, EmployeeDto>();
        CreateMap<EmployeeDto, EmployeeResponse>();
        CreateMap<EmployeeDto, EmployeeDetailResponse>();
        CreateMap<EmployeeDto, Employee>();
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AppUser.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.AppUser.Surname));


        CreateMap<EmployeeExpenseReportDto, GetEmployeeExpenseReportQueryResponse>();
        CreateMap<EmployeeSpendingReportDto, GetEmployeeSpendingReportQueryResponse>();
        CreateMap<CategoryExpenseReportDto, GetCategoryExpenseReportQueryResponse>();
        CreateMap<PaymentReportDto, GetPaymentsReportQueryResponse>();
    }
}