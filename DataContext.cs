using MappingByExtensionMethod.Entities;
using Microsoft.EntityFrameworkCore;

namespace MappingByExtensionMethod;

public sealed class DataContext(DbContextOptions<DataContext> options):DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
}