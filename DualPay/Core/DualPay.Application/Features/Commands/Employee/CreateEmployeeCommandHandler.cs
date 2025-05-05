using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.Features.Queries;
using DualPay.Domain.Entities;
using DualPay.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DualPay.Application.Features.Commands;
public class CreateEmployeeCommandHandler:IRequestHandler<CreateEmployeeCommandRequest,ApiResponse<EmployeeResponse>>
{
    private IEmployeeService _employeeService;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeCommandHandler(IMapper mapper, UserManager<AppUser> userManager, IEmployeeService employeeService, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _userManager = userManager;
        _employeeService = employeeService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<EmployeeResponse>> Handle(CreateEmployeeCommandRequest request, CancellationToken cancellationToken)
    {
        
        var user = new AppUser
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            EmailConfirmed = true,
            UserName = request.Name.ToLower() + "." + request.Name.ToLower(),
            AccountNumber = request.AccountNumber,
            Balance = 2000
        };
        try
        {
            var result = await _userManager.CreateAsync(user, "123"); //need to hash


            if (result.Succeeded)
            {
                var employee = new Employee
                {
                    UserId = user.Id,
                    IdentityNumber = request.IdentityNumber,
                    PhoneNumber = request.PhoneNumber,
                    AccountNumber = request.AccountNumber,
                };
                await _unitOfWork.GetRepository<Employee>().AddAsync(employee);
                await _userManager.AddToRoleAsync(user, "USER");

                // var entity = _mapper.Map<Employee>(request);
                // entity.UserId = user.Id;
                // var data = await _employeeRepository.AddAsync(entity);

                //var response = _mapper.Map<EmployeeResponse>(data);
                //apiResponse.Data = response;

               await _unitOfWork.Complete();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            if (await _userManager.FindByIdAsync(user.Id.ToString()) != null)
            {
                await _userManager.DeleteAsync(user);
            }

            throw e;
        }

        return null;
    }
}

public class CreateEmployeeCommandRequest : IRequest<ApiResponse<EmployeeResponse>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string AccountNumber { get; set; }
    public string IdentityNumber { get; set; }

}