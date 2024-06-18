using Microsoft.AspNetCore.Mvc;
using WebApiAuth.Data;
using WebApiAuth.Models;
using WebApiAuth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace WebApiAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IRecordService _authServices;
        private readonly ILogger<RecordController> _logger;

        public RecordController(AppDbContext context, IRecordService authService, ILogger<RecordController> logger)
        {
            _context = context;
            _authServices = authService;
            _logger = logger;
        }

        [HttpPost("CreateRegister")]
        [AllowAnonymous]
        public async Task<IActionResult> Post(TblUser user)
        {
            _logger.LogInformation("Post Api Run");
            var data = await _authServices.CreateRegister(user);
            if (data)
            {
                _logger.LogInformation("Post Api Run Successfully");
                return Ok("Record Save successfully");
            }
            _logger.LogInformation("Post Api Generate Bad Request");
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpGet("GetAll")]
        [ServiceFilter(typeof(CustomAuthenticationFilter))] // Apply the custom filter
        public async Task<IActionResult> GetAll()
        {
            var data = await _authServices.GetAll();
            return Ok(data);
        }

        [HttpDelete("DeleteById")]
        [ServiceFilter(typeof(CustomAuthenticationFilter))] // Apply the custom filter
        public async Task<IActionResult> DeleteById(int id)
        {
            await _authServices.DeleteById(id);
            return NoContent();
        }


        // API Action method created for checking the action, result and exception filter are working or not.

        [HttpGet]
        [ServiceFilter(typeof(CustomAuthenticationFilter))]
        [ServiceFilter(typeof(CustomResultFilter))]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client, VaryByHeader = "User-Agent")] // Cache the response for 60 seconds

        public IActionResult Get()
        {
            Console.WriteLine("Executed One time");
            return Ok("Hello_World");
        }

         [HttpGet("trigger-exception")]
        public IActionResult TriggerException()
        {
            throw new Exception("This is a test exception.");
        }
    }
}
