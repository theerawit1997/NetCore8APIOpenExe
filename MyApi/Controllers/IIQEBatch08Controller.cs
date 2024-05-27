using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IIQEBatch08Controller : Controller
    {
        private const string LogFilePath = @"C:\LogFileOpenExe\log.txt";

        [HttpGet]
        public IActionResult IIQEBatch08()
        {
            try
            {
                // Log or return message indicating work started
                //Console.WriteLine("IIQEBatch01!!");
                LogMessage("IIQEBatch08!!");
                string exePath = @"C:\inetpub\wwwroot\IIQE Batch\08 IIQE_Transfer_DataToAG_Process\IIQE_Transfer_DataToAG_Process.exe";
                //Console.WriteLine($"exePath: {exePath}");
                //LogMessage($"exePath: {exePath}");

                if (!System.IO.File.Exists(exePath))
                {
                    LogError("Executable file not found.");
                    return NotFound("Executable file not found.");
                }

                // Log or return message indicating work started
                //Console.WriteLine("Exe started.");
                LogMessage("Exe started.");

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = exePath;
                    process.Start();
                    process.WaitForExit(); // Wait for the executable to finish
                }

                // Log or return message indicating work finished
                //Console.WriteLine("Exe finished.");
                LogMessage("Exe finished.");

                return Ok("Executable started and completed successfully.");
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error: {ex.Message}");
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
