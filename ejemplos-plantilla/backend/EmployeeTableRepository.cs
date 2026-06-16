using Demo.Infrastructure.Entities;

namespace Demo.Tables.Employees;

public interface IEmployeeTableRepository
{
    IQueryable<Employee> GetQueryable();
}

public sealed class EmployeeTableRepository : IEmployeeTableRepository
{
    private readonly AppDbContext _db;

    public EmployeeTableRepository(AppDbContext db) => _db = db;

    public IQueryable<Employee> GetQueryable() => _db.Employees.AsNoTracking();
}
