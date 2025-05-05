using System.Linq.Expressions;
using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;
using DualPay.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace DualPay.Application.Services;
public class EmployeeService : IEmployeeService
{
    private IGenericRepository<Employee> _employeeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
        _employeeRepository = unitOfWork.GetRepository<Employee>();
    }

    public async Task<List<EmployeeDto>> GetAllAsync(params string[] includes)
    {
        List<Employee> datas = await _employeeRepository.GetAllAsync(includes);
        return _mapper.Map<List<EmployeeDto>>(datas);
    }
    public async Task<List<EmployeeDto>> GetAllAsync(Expression<Func<Employee, bool>> predicate, params string[] includes)
    {
        List<Employee> datas = await _employeeRepository.GetAllAsync(predicate, includes);
        return _mapper.Map<List<EmployeeDto>>(datas);
    }

    public async Task<List<EmployeeDto>> Where(Expression<Func<Employee, bool>> predicate, params string[] includes)
    {
        List<Employee> datas = await _employeeRepository.Where(predicate,includes);
        return _mapper.Map<List<EmployeeDto>>(datas);
    }

    public async Task<EmployeeDto> GetByIdAsync(int id, params string[] includes)
    {
        Employee data = await  _unitOfWork.GetRepository<Employee>().GetByIdAsync(id,includes);
        return _mapper.Map<EmployeeDto>(data);
    }

    public async Task<EmployeeDto> AddAsync(EmployeeDto employeeDto)
    {
        _employeeRepository = _unitOfWork.GetRepository<Employee>();
        // var user = new AppUser
        // {
        //     Name = request.Name,
        //     Surname = request.Surname,
        //     Email = request.Email,
        //     EmailConfirmed = true,
        //     UserName = request.Name.ToLower() + "." + request.Name.ToLower(),
        // };
        try
        {
            //var result = await _userManager.CreateAsync(user, "123"); //need to hash


            //if (result.Succeeded)
            //{
                var employee = new Employee
                {
                    //UserId = user.Id,
                    IdentityNumber = employeeDto.IdentityNumber,
                    PhoneNumber = employeeDto.PhoneNumber,
                };
                await _employeeRepository.AddAsync(employee);
                //await _userManager.AddToRoleAsync(user, "USER");

                // var entity = _mapper.Map<Employee>(request);
                // entity.UserId = user.Id;
                // var data = await _employeeRepository.AddAsync(entity);

                //var response = _mapper.Map<EmployeeResponse>(data);
                //apiResponse.Data = response;

                await _unitOfWork.Complete();
            //}
        }
        catch (Exception e)
        {
            // Console.WriteLine(e);
            // if (await _userManager.FindByIdAsync(user.Id.ToString()) != null)
            // {
            //     await _userManager.DeleteAsync(user);
            // }

            throw e;
        }

        return null;
    }

    public async Task UpdateAsync(EmployeeDto employeeDto)
    {
        Employee employee = _mapper.Map<Employee>(employeeDto);
        _unitOfWork.GetRepository<Employee>().Update(employee);
        await _unitOfWork.Complete();
    }
    
    public async Task DeleteByIdAsync(int id)
    {
        await _unitOfWork.GetRepository<Employee>().DeleteByIdAsync(id);
        await _unitOfWork.Complete();
    }
}