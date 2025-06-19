using EmployeeWithJWT.Helpers;
using EmployeeWithJWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly EmployeeWithJwtContext _context;

        public AuthController(EmployeeWithJwtContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ✅ LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Models.LoginRequest request)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e =>
                    e.Username == request.UsernameOrEmail || e.Email == request.UsernameOrEmail);

            if (employee == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var passwordHelper = new PasswordHelper();
            bool isValid = passwordHelper.VerifyPassword(employee.PasswordHash, request.Password);

            if (!isValid)
            {
                return Unauthorized("Invalid credentials.");
            }

            var jwtHelper = new JwtTokenHelper(_configuration);
            var token = jwtHelper.GenerateToken(employee);

            return Ok(new { Token = token, Message = "Login successful." });
        }

        // ✅ REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _context.Employees.AnyAsync(e => e.Username == request.Username || e.Email == request.Email))
            {
                return BadRequest("Username or Email already exists.");
            }

            var passwordHelper = new PasswordHelper();
            string hashedPassword = passwordHelper.HashPassword(request.Password);

            var employee = new Employee
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = hashedPassword,
                FullName = request.FullName,
                Gender = request.Gender,
                Dob = request.Dob,
                StateId = request.StateId,
                DepartmentId = request.DepartmentId,
                JoiningDate = request.JoiningDate,
                CreatedAt = DateTime.UtcNow
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Registration successful.",
                EmployeeId = employee.EmployeeId
            });
        }
    }

    // ✅ PasswordHelper: centralized and reusable
    public class PasswordHelper
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
