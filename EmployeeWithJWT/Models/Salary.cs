using System;
using System.Collections.Generic;

namespace EmployeeWithJWT.Models;

public partial class Salary
{
    public int SalaryId { get; set; }

    public int EmployeeId { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal? Hra { get; set; }

    public decimal? Allowance { get; set; }

    public decimal? Deduction { get; set; }

    public DateOnly SalaryDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
