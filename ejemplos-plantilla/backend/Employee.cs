namespace Demo.Infrastructure.Entities;

public sealed class Employee
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public DateTime? BirthDate { get; set; }
    public string EmploymentStatus { get; set; } = string.Empty;
    public bool HasHouse { get; set; }
    public string StatusColorHex { get; set; } = "#999999";
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
