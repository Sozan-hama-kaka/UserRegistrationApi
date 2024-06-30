// Controllers/IndustryController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserRegistrationApi.Data;
using UserRegistrationApi.Models;

namespace UserRegistrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndustryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IndustryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Industry>>> GetIndustries()
        {
            return await _context.Industries.ToListAsync();
        }
    }
}
