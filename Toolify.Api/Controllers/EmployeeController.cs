    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Toolify.Core.Entities.Dtos;
    using Toolify.DataAccess.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.OpenApi.Writers;
    using Microsoft.AspNetCore.Http.HttpResults;
    using System.Data;
    using System.Diagnostics.Metrics;
    using System.Numerics;
    namespace ToolifyAPI.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class EmployeeController : ControllerBase
        {
            private readonly IConfiguration _config;
            private readonly ApplicationDbContext _context;

            public EmployeeController(IConfiguration config, ApplicationDbContext context)
            {

                _config = config;
                _context = context;
            }


            [HttpPost("add-employee")]
            public async Task<IActionResult> AddEmployees([FromBody] EmployeeDto request)
            {
                if (request == null)
                    return BadRequest("Request cannot be null or empty.");

            var employees = new EmployeeModels
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                City = request.City,
                State = request.State,
                Country = request.Country,
                Type = request.Type,
                Vendor = request.Vendor,
                Role = request.Role,
                Software1 = request.Software1,
                Software2 = request.Software2,
                Software3 = request.Software3,
                Software4 = request.Software4,
                Software5 = request.Software5,
                Software6 = request.Software6,
                Software7 = request.Software7,
                Software8 = request.Software8,
                Software9 = request.Software9,
                Software10 = request.Software10,
                WorkEmail = request.WorkEmail,
                PersonalEmail = request.PersonalEmail,
                Phone = request.Phone,
                CreatedBy = request.CreatedBy,
                CreatedDate = request.CreatedDate,
                UpdatedBy = request.UpdatedBy,
                UpdatedDate = request.UpdatedDate
            };

                _context.Employees.AddRange(employees);
                await _context.SaveChangesAsync();

                return Ok("Employees added successfully.");
            }



            [HttpGet("all-employees")]
            //  [Authorize(Roles ="Admin")]
            public async Task<IActionResult> GetAllEmployees()
            {

                var employees = await _context.Employees
                    .Select(e => new EmployeeDto
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        City = e.City,
                        State = e.State,
                        Country = e.Country,
                        Type = e.Type,
                        Vendor = e.Vendor,
                        Role = e.Role,
                        Software1 = e.Software1,
                        Software2 = e.Software2,
                        Software3 = e.Software3,
                        Software4 = e.Software4,
                        Software5 = e.Software5,
                        Software6 = e.Software6,
                        Software7 = e.Software7,
                        Software8 = e.Software8,
                        Software9 = e.Software9,
                        Software10 = e.Software10,
                        WorkEmail = e.WorkEmail,
                        PersonalEmail = e.PersonalEmail,
                        Phone = e.Phone,
                        CreatedBy = e.CreatedBy,
                        CreatedDate = e.CreatedDate,
                        UpdatedBy = e.UpdatedBy,
                        UpdatedDate = e.UpdatedDate
                    })
                    .ToListAsync();

                return Ok(employees);
            }
            [HttpGet("employee-details")]
            public async Task<IActionResult> GetEmployeeByName([FromQuery] string firstName)
            {
                if (string.IsNullOrWhiteSpace(firstName))
                    return BadRequest("First name is required.");

                var employee = await _context.Employees
                    .Where(u => (u.FirstName ?? "") == firstName)
                    .Select(e => new EmployeeDto
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        City = e.City,
                        State = e.State,
                        Country = e.Country,
                        Type = e.Type,
                        Vendor = e.Vendor,
                        Role = e.Role,
                        Software1 = e.Software1,
                        Software2 = e.Software2,
                        Software3 = e.Software3,
                        Software4 = e.Software4,
                        Software5 = e.Software5,
                        Software6 = e.Software6,
                        Software7 = e.Software7,
                        Software8 = e.Software8,
                        Software9 = e.Software9,
                        Software10 = e.Software10,
                        WorkEmail = e.WorkEmail,
                        PersonalEmail = e.PersonalEmail,
                        Phone = e.Phone,
                        CreatedBy = e.CreatedBy,
                        CreatedDate = e.CreatedDate,
                        UpdatedBy = e.UpdatedBy,
                        UpdatedDate = e.UpdatedDate
                    })
                    .FirstOrDefaultAsync();

                if (employee == null)
                    return NotFound($"No employee found with name {firstName}.");

                return Ok(employee);
            }

            [HttpPut("update-employee/{id}")]
            // [Authorize(Roles = "Admin")]
            public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto updatedEmployee)
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                    return NotFound($"Employee with ID {id} not found.");

                // update fields
                employee.FirstName = updatedEmployee.FirstName;
                employee.LastName = updatedEmployee.LastName;
                employee.City = updatedEmployee.City;
                employee.State = updatedEmployee.State;
                employee.Country = updatedEmployee.Country;
                employee.Type = updatedEmployee.Type;
                employee.Vendor = updatedEmployee.Vendor;
                employee.Role = updatedEmployee.Role;
                employee.Software1 = updatedEmployee.Software1;
                employee.Software2 = updatedEmployee.Software2;
                employee.Software3 = updatedEmployee.Software3;
                employee.Software4 = updatedEmployee.Software4;
                employee.Software5 = updatedEmployee.Software5;
                employee.Software6 = updatedEmployee.Software6;
                employee.Software7 = updatedEmployee.Software7;
                employee.Software8 = updatedEmployee.Software8;
                employee.Software9 = updatedEmployee.Software9;
                employee.Software10 = updatedEmployee.Software10;
                employee.WorkEmail = updatedEmployee.WorkEmail;
                employee.PersonalEmail = updatedEmployee.PersonalEmail;
                employee.Phone = updatedEmployee.Phone;
                employee.CreatedBy = updatedEmployee.CreatedBy;
                employee.CreatedDate = updatedEmployee.CreatedDate;
                employee.UpdatedBy = updatedEmployee.UpdatedBy;
                employee.UpdatedDate = updatedEmployee.UpdatedDate;


                await _context.SaveChangesAsync();

                return Ok("Employee updated successfully.");
            }
            [HttpDelete("delete-employee/{id}")]
            public async Task<IActionResult> DeleteEmployee(int id)
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                    return NotFound($"Employee with ID {id} not found.");

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                return Ok($"Employee with ID {id} deleted successfully.");
            }
          }
       }



