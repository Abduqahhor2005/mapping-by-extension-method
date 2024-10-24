using MappingByExtensionMethod.Entities;
using MappingByExtensionMethod.Filter;
using MappingByExtensionMethod.Response;

namespace MappingByExtensionMethod.Service;

public class EmployeeService(DataContext context) : IEmployeeService
{
    public PaginationResponse<IEnumerable<ReadEmployee>> GetAllEmployees(EmployeeFilter filter)
    {
        IQueryable<Employee> employees = context.Employees;
        if (filter.Name != null)
            employees = employees.Where(x => x.Name.ToLower()
                .Contains(filter.Name.ToLower()));
        if (filter.Age != null)
            employees = employees.Where(x => x.Age == filter.Age);
        if (filter.Email != null)
            employees = employees.Where(x => x.Email!.ToLower()
                .Contains(filter.Email.ToLower()));
        if (filter.PhoneNumber != null)
            employees = employees.Where(x => x.PhoneNumber!.ToLower()
                .Contains(filter.PhoneNumber.ToLower()));
        IQueryable<ReadEmployee> result = employees.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize).Select(x=>x.ReadToEmployee());
        int totalRecords = context.Employees.Count();
        return PaginationResponse<IEnumerable<ReadEmployee>>.Create(filter.PageNumber, filter.PageSize, totalRecords, result);
    }

    public ReadEmployee GetEmployeeById(int id)
    {
        var employee = (from u in context.Employees
            where u.IsDeleted == false && u.Id==id
            select u.ReadToEmployee()).FirstOrDefault();
        return employee;
    }

    public bool CreateEmployee(CreateEmployee createEmployee)
    {
        bool existEmployee = context.Employees.Any(x =>
            x.Name.ToLower() == createEmployee.Name.ToLower() && x.IsDeleted == false);
        if (existEmployee) return false;
        context.Employees.Add(createEmployee.CreateToEmployee());
        context.SaveChanges();
        return true;
    }

    public bool UpdateEmployee(UpdateEmployee updateEmployee)
    {
        Employee? employee = context.Employees.FirstOrDefault(x => x.IsDeleted == false && x.Id == updateEmployee.Id);
        if (employee is null) return false;
        context.Employees.Update(employee.UpdateToEmployee(updateEmployee));
        context.SaveChanges();
        return true;
    }

    public bool DeleteEmployee(int id)
    {
        Employee? employee = context.Employees.FirstOrDefault(x => x.Id == id);
        if (employee is null) return false;
        employee.DeleteToEmployee();
        context.SaveChanges();
        return true;
    }
}