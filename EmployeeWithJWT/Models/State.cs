using System;
using System.Collections.Generic;

namespace EmployeeWithJWT.Models;

public partial class State
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
