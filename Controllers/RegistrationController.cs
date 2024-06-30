using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationApi.Data;
using UserRegistrationApi.Models;

namespace UserRegistrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<RegistrationController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(ms => ms.Value.Errors.Select(e => e.ErrorMessage)).ToList();
                return BadRequest(new { Errors = errors });
            }

            // Check if the login (username) is unique
            var existingUser = await _userManager.FindByNameAsync(model.Login);
            if (existingUser != null)
            {
                return BadRequest(new { Errors = new[] { "Login already exists. Please choose a different login." } });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var industry = await _context.Industries.FindAsync(model.IndustryId);
                if (industry == null)
                {
                    return BadRequest(new { Errors = new[] { "Invalid Industry ID." } });
                }

                var company = new CompanyInfo
                {
                    Name = model.CompanyName,
                    Industry = industry.Name
                };
                _context.CompanyInfos.Add(company);
                await _context.SaveChangesAsync();

                var user = new ApplicationUser
                {
                    UserName = model.Login,
                    Email = model.Email,
                    Name = model.UserName
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return BadRequest(new { Errors = errors });
                }

                var userInfo = new UserInfo
                {
                    UserId = user.Id,
                    FirstName = model.FirstName
                };
                _context.UserInfos.Add(userInfo);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return Ok(new { Message = "Registration successful" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "An error occurred while registering user.");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
