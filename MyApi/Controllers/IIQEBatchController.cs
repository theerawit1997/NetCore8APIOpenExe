using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IIQEBatchController : Controller
    {
        private const string LogFilePath = @"C:\LogFileOpenExe\log.txt";

        [HttpGet("IIQEBatch001")]
        public IActionResult IIQEBatch001()
        {
            return ExecuteBatch(
                "IIQEBatch01",
                @"C:\inetpub\wwwroot\IIQE Batch\01 P_Exam_Seat_Number_Process\P_Exam_Seat_Number_Process.exe"
            );
        }

        [HttpGet("IIQEBatch02")]
        public IActionResult IIQEBatch002()
        {
            return ExecuteBatch(
                "IIQEBatch02",
                @"C:\inetpub\wwwroot\IIQE Batch\02 IIQE_SEND_P_EXAM_ROUND\IIQE_SEND_P_EXAM_ROUND.exe"
            );
        }

        private IActionResult ExecuteBatch(string batchName, string exePath)
        {
            try
            {
                LogMessage($"{batchName} started.");

                if (!System.IO.File.Exists(exePath))
                {
                    LogError("Executable file not found.");
                    return NotFound("Executable file not found.");
                }

                LogMessage("Exe started.");

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = exePath;
                    process.Start();
                    process.WaitForExit(); // Wait for the executable to finish
                }

                LogMessage("Exe finished.");

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
                EnsureLogDirectoryExists();
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

        private static void EnsureLogDirectoryExists()
        {
            string directory = Path.GetDirectoryName(LogFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
