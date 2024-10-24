namespace MappingByExtensionMethod.Entities;

public class Employee : BaseEntity
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}