using MappingByExtensionMethod.Filter;
using MappingByExtensionMethod.Response;

namespace MappingByExtensionMethod.Service;

public interface IEmployeeService
{
    PaginationResponse<IEnumerable<ReadEmployee>> GetAllEmployees(EmployeeFilter filter);
    ReadEmployee GetEmployeeById(int id);
    bool CreateEmployee(CreateEmployee employee);
    bool UpdateEmployee(UpdateEmployee employee);
    bool DeleteEmployee(int id);
}