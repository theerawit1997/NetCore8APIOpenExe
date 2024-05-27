using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private const string LogFilePath = @"D:\WTH This Code\test vscommu2\MyApi\MyApi\Content\log.txt";

        [HttpGet]
        public IActionResult Test()
        {
            try
            {
                // Log or return message indicating work started
                Console.WriteLine("Test!!");
                LogMessage("Test!!");
                string exePath = @"D:\WTH This Code\test vscommu\testBasic\testBasic\bin\Debug\app.publish\testBasic.exe";
                //Console.WriteLine($"exePath: {exePath}");
                //LogMessage($"exePath: {exePath}");

                if (!System.IO.File.Exists(exePath))
                {
                    LogError("Executable file not found.");
                    return NotFound("Executable file not found.");
                }

                // Log or return message indicating work started
                Console.WriteLine("Work started.");
                LogMessage("Work started.");

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = exePath;
                    process.Start();
                    process.WaitForExit(); // Wait for the executable to finish
                }

                // Log or return message indicating work finished
                Console.WriteLine("Work finished.");
                LogMessage("Work finished.");

                return Ok("Executable started and completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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
                    string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}";
                    writer.WriteLine(logEntry);
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
