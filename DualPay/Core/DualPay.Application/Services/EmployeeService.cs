using System.Linq.Expressions;
using AutoMapper;
using DualPay.Application.Abstraction;
using DualPay.Application.Abstraction.Services;
using DualPay.Application.DTOs;
using DualPay.Domain.Entities;

namespace DualPay.Application.Services;
public class EmployeeService : IEmployeeService
{
    private IGenericRepository<Employee> _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
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
        try
        {
                var employee = new Employee
                {
                    IdentityNumber = employeeDto.IdentityNumber,
                    PhoneNumber = employeeDto.PhoneNumber,
                };
                await _employeeRepository.AddAsync(employee);
                await _unitOfWork.Complete();
        }
        catch (Exception e)
        {
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