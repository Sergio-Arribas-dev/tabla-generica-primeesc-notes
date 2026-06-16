using ECS.PrimengTable.Attributes;
using ECS.PrimengTable.Enums;

namespace Demo.Tables.Employees;

public sealed class EmployeeTableDto
{
    [ColumnAttributes(sendColumnAttributes: false)]
    public Guid RowID { get; set; }

    [ColumnAttributes("Usuario", canBeGlobalFiltered: true)]
    public string Username { get; set; } = string.Empty;

    [ColumnAttributes("Salario", dataType: DataType.Numeric, dataAlignHorizontal: DataAlignHorizontal.Right)]
    public decimal Salary { get; set; }

    [ColumnAttributes("Fecha nacimiento", dataType: DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [ColumnAttributes("Estado", filterPredefinedValuesName: "employmentStatus")]
    public string EmploymentStatus { get; set; } = string.Empty;

    [ColumnAttributes("Casa", dataType: DataType.Boolean)]
    public bool HasHouse { get; set; }

    [ColumnAttributes(sendColumnAttributes: false)]
    public string StatusColorHex { get; set; } = "#999999";
}
