using System.Linq.Expressions;
using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.Common.Models;
using DualPay.Application.Common.Models.Requests;
using DualPay.Application.Models.Responses;
using DualPay.Domain.Entities;
using DualPay.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DualPay.Application.Services;

public class EmployeeService : IEmployeeService
{
    private IGenericRepository<Employee> _employeeRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _employeeRepository = unitOfWork.GetRepository<Employee>();
    }

    public ApiResponse<List<EmployeeResponse>> GetAll(bool tracking)
    {
        var datas = _employeeRepository.GetAll(tracking).ToList();
        var mapped = _mapper.Map<List<EmployeeResponse>>(datas);

        var apiResponse = new ApiResponse<List<EmployeeResponse>>(mapped);
        apiResponse.Success = true;
        return apiResponse;
    }

    public ApiResponse<List<EmployeeResponse>> GetWhere(Expression<Func<Employee, bool>> method, bool tracking = true)
    {
        var datas = _employeeRepository.GetWhere(method, tracking).ToList();
        var mapped = _mapper.Map<List<EmployeeResponse>>(datas);

        var apiResponse = new ApiResponse<List<EmployeeResponse>>(mapped);
        apiResponse.Success = true;
        return apiResponse;
    }

    public async Task<ApiResponse<EmployeeResponse>> GetByIdAsync(int id, bool tracking = true)
    {
        var data = await _employeeRepository.GetByIdAsync(id, tracking);
        var mapped = _mapper.Map<EmployeeResponse>(data);

        var apiResponse = new ApiResponse<EmployeeResponse>(mapped);
        apiResponse.Success = true;
        return apiResponse;
    }

    public async Task<ApiResponse<EmployeeResponse>> AddAsync(CreateEmployeeRequest request)
    {
        _employeeRepository = _unitOfWork.GetRepository<Employee>();
        var user = new AppUser
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            EmailConfirmed = true,
            UserName = request.Name.ToLower() + "." + request.Name.ToLower(),
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
                };
                await _employeeRepository.AddAsync(employee);
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

    public ApiResponse<object> Update(UpdateEmployeeRequest request)
    {
        var entity = _mapper.Map<Employee>(request);
        _employeeRepository.Update(entity);
        return new ApiResponse<object>() { Success = true };
    }

    public async Task DeleteByIdAsync(int id)
    {
        await _employeeRepository.DeleteByIdAsync(id);
    }
}