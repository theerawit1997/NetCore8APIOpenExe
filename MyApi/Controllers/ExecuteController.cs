using Microsoft.AspNetCore.Mvc;
using System;
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
                string exePath = @"D:\WTH This Code\test vscommu2\MyApi\MyApi\Content\testBasic.exe";

                if (!System.IO.File.Exists(exePath))
                {
                    return NotFound("Executable file not found.");
                }

                // Log or return message indicating work started
                Console.WriteLine("Work started.");
                // or return Ok("Work started.");

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = exePath;
                    process.Start();
                    process.WaitForExit(); // Wait for the executable to finish
                }

                // Log or return message indicating work finished
                Console.WriteLine("Work finished.");
                // or return Ok("Work finished.");

                return Ok("Executable started and completed successfully.");
            }
            catch (Exception ex)
            {
                // Log or return error message
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
