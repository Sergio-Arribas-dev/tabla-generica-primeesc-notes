using ECS.PrimengTable.Enums;
using ECS.PrimengTable.Models;
using ECS.PrimengTable.Services;

namespace Demo.Tables.Employees;

public interface IEmployeeTableService
{
    TableConfigurationModel GetTableConfiguration();
    (bool success, string? error, TablePagedResponseModel? data) GetTableData(TableQueryRequestModel request);
}

public sealed class EmployeeTableService : IEmployeeTableService
{
    private static readonly int[] AllowedPageSizes = [10, 25, 50, 100];
    private static readonly List<string> DefaultSortColumns = [nameof(EmployeeTableDto.Username)];
    private static readonly List<ColumnSort> DefaultSortOrder = [ColumnSort.Ascending];

    private readonly IEmployeeTableRepository _repository;

    public EmployeeTableService(IEmployeeTableRepository repository) => _repository = repository;

    public TableConfigurationModel GetTableConfiguration()
    {
        return EcsPrimengTableService.GetTableConfiguration<EmployeeTableDto>(
            allowedItemsPerPage: AllowedPageSizes,
            dateFormat: "dd/MM/yyyy HH:mm",
            dateTimezone: "+01:00",
            dateCulture: "es-ES",
            exportDateFormat: "dd/mm/yyyy hh:mm"
        );
    }

    public (bool success, string? error, TablePagedResponseModel? data) GetTableData(TableQueryRequestModel request)
    {
        if (!EcsPrimengTableService.ValidateItemsPerPageAndCols(request.PageSize, request.Columns?.ToList(), AllowedPageSizes))
            return (false, "PageSize o columnas inválidas", null);

        var result = EcsPrimengTableService.PerformDynamicQuery(
            inputData: request,
            baseQuery: BuildBaseQuery(),
            defaultSortColumnName: DefaultSortColumns,
            defaultSortOrder: DefaultSortOrder
        );

        return (true, null, result);
    }

    private IQueryable<EmployeeTableDto> BuildBaseQuery()
    {
        return _repository.GetQueryable().Select(x => new EmployeeTableDto
        {
            RowID = x.Id,
            Username = x.Username,
            Salary = x.Salary,
            BirthDate = x.BirthDate,
            EmploymentStatus = x.EmploymentStatus,
            HasHouse = x.HasHouse,
            StatusColorHex = x.StatusColorHex
        });
    }
}
