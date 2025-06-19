using System;
using System.Collections.Generic;

namespace EmployeeWithJWT.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public int? StateId { get; set; }

    public int? DepartmentId { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual State? State { get; set; }
}
public class LoginRequest
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}
public class RegisterRequest
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string? Gender { get; set; }
    public DateOnly? Dob { get; set; }
    public int StateId { get; set; }
    public int DepartmentId { get; set; }
    public DateOnly? JoiningDate { get; set; }
}
