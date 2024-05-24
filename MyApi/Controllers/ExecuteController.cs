using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecuteController : ControllerBase
    {
        [HttpGet]
        public IActionResult ExecuteFile()
        {
            try
            {
                //https://localhost:7131/api/Execute
                // Provide the path to your executable file
                string exePath = @"D:\WTH This Code\test vscommu2\MyApi\MyApi\Content\testBasic.exe";
                Process.Start(exePath);
                return Ok("Executable started successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
