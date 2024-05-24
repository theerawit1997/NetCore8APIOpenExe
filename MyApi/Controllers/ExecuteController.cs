using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecuteController : ControllerBase
    {
        private const string LogFilePath = @"D:\WTH This Code\test vscommu2\MyApi\MyApi\Content\log.txt";

        [HttpGet]
        public IActionResult ExecuteFile()
        {
            try
            {
                string exePath = @"D:\WTH This Code\test vscommu2\MyApi\MyApi\Content\testBasic.exe";

                if (!System.IO.File.Exists(exePath))
                {
                    LogError("Executable file not found.");
                    return NotFound("Executable file not found.");
                }

                LogMessage("Work started.");

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = exePath;
                    process.Start();
                    process.WaitForExit(); // Wait for the executable to finish
                }

                LogMessage("Work finished.");

                return Ok("Executable started and completed successfully.");
            }
            catch (Exception ex)
            {
                LogError($"Error: {ex.Message}");
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        private static void LogMessage(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(LogFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while writing to log file: {ex.Message}");
            }
        }

        private static void LogError(string errorMessage)
        {
            LogMessage($"ERROR: {errorMessage}");
        }
    }
}
