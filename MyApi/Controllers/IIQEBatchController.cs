using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IIQEBatchController : ControllerBase
    {
        private const string LogFilePath = @"C:\LogFileOpenExe\log.txt";

        [HttpGet("IIQEBatch001")]
        public IActionResult IIQEBatch001()
        {
            return ExecuteBatch(
                "IIQEBatch001",
                @"C:\inetpub\wwwroot\IIQE Batch\01 P_Exam_Seat_Number_Process\P_Exam_Seat_Number_Process.exe"
            );
        }

        [HttpGet("IIQEBatch002")]
        public IActionResult IIQEBatch002()
        {
            return ExecuteBatch(
                "IIQEBatch002",
                @"C:\inetpub\wwwroot\IIQE Batch\02 IIQE_SEND_P_EXAM_ROUND\IIQE_SEND_P_EXAM_ROUND.exe"
            );
        }

        [HttpGet("IIQEBatch003")]
        public IActionResult IIQEBatch003()
        {
            return ExecuteBatch(
                "IIQEBatch003",
                @"C:\inetpub\wwwroot\IIQE Batch\03 IIQE_SEND_C_EXAM_ROUND\IIQE_SEND_C_EXAM_ROUND.exe"
            );
        }

        [HttpGet("IIQEBatch004")]
        public IActionResult IIQEBatch004()
        {
            return ExecuteBatch(
                "IIQEBatch004",
                @"C:\inetpub\wwwroot\IIQE Batch\04 P_Exam_Result_Transfer_Process\P_Exam_Result_Transfer_Process.exe"
            );
        }

        [HttpGet("IIQEBatch005")]
        public IActionResult IIQEBatch005()
        {
            return ExecuteBatch(
                "IIQEBatch005",
                @"C:\inetpub\wwwroot\IIQE Batch\05 C_Exam_Result_Transfer_Process\C_Exam_Result_Transfer_Process.exe"
            );
        }

        [HttpGet("IIQEBatch006")]
        public IActionResult IIQEBatch006()
        {
            return ExecuteBatch(
                "IIQEBatch006",
                @"C:\inetpub\wwwroot\IIQE Batch\06 IIQE_TRANSFER_P_EXAM_ROUND_TO_AG\IIQE_TRANSFER_P_EXAM_ROUND_TO_AG.exe"
            );
        }

        [HttpGet("IIQEBatch007")]
        public IActionResult IIQEBatch007()
        {
            return ExecuteBatch(
                "IIQEBatch007",
                @"C:\inetpub\wwwroot\IIQE Batch\07 IIQE_TRANSFER_C_EXAM_ROUND_TO_AG\IIQE_TRANSFER_C_EXAM_ROUND_TO_AG.exe"
            );
        }

        [HttpGet("IIQEBatch008")]
        public IActionResult IIQEBatch008()
        {
            return ExecuteBatch(
                "IIQEBatch008",
                @"C:\inetpub\wwwroot\IIQE Batch\08 IIQE_Transfer_DataToAG_Process\IIQE_Transfer_DataToAG_Process.exe"
            );
        }

        [HttpGet("IIQEBatch009")]
        public IActionResult IIQEBatch009()
        {
            return ExecuteBatch(
                "IIQEBatch009",
                @"C:\inetpub\wwwroot\IIQE Batch\09 Lookup_Data_From_OIC_API\Lookup_Data_From_OIC_API.exe"
            );
        }

        [HttpGet("IIQEBatch010")]
        public IActionResult IIQEBatch010()
        {
            return ExecuteBatch(
                "IIQEBatch010",
                @"C:\inetpub\wwwroot\IIQE Batch\FixMigration\Migrate_Data_AG_TO_IIQE.exe"
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
