using System.Linq.Expressions;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Domain.Entities;
using DualPay.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace DualPay.Application.Services;
public class EmployeeService : IEmployeeService
{
    private IGenericRepository<Employee> _employeeRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _employeeRepository = unitOfWork.GetRepository<Employee>();
    }

    public async Task<List<Employee>> GetAllAsync(params string[] includes)
    {
        var datas = await _employeeRepository.GetAllAsync(includes);
        return datas;
    }
    public async Task<List<Employee>> GetAllAsync(Expression<Func<Employee, bool>> predicate, params string[] includes)
    {
        var datas = await _employeeRepository.GetAllAsync(predicate, includes);
        return datas;
    }

    public async Task<List<Employee>> Where(Expression<Func<Employee, bool>> predicate, params string[] includes)
    {
        var datas = await _employeeRepository.Where(predicate,includes);
        return datas;
    }

    public async Task<Employee> GetByIdAsync(int id, params string[] includes)
    {
        var data = await _employeeRepository.GetByIdAsync(id);
        return data;
    }

    public async Task<Employee> AddAsync(Employee request)
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
                    IdentityNumber = request.IdentityNumber,
                    PhoneNumber = request.PhoneNumber,
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

    public async Task UpdateAsync(Employee entity)
    {
        await _employeeRepository.GetByIdAsync(entity.Id);
    }


    public async Task DeleteByIdAsync(int id)
    {
        await _employeeRepository.DeleteByIdAsync(id);
    }
}