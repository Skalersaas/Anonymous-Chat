using Microsoft.AspNetCore.Mvc;
using Anonymous_Chat.Handlers;

namespace Anonymous_Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChattingController : ControllerBase
    {
        [Route("connect")]
        public async Task Connect()
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await ConnectionHandler.HandleConnectionAsync(webSocket);
        }
        [HttpPost]
        [Route("upload-file")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile model)
        {
            if (model == null || model.Length == 0)
                return BadRequest("Invalid file.");
            string tempDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tempFiles");

            if (!Directory.Exists(tempDirectory))
                Directory.CreateDirectory(tempDirectory);

            string filePath = Path.Combine(tempDirectory, Guid.NewGuid().ToString() + Path.GetExtension(model.FileName));
            // Save the file to the temporary location
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.CopyToAsync(stream);
            }

            string fileName = Path.GetFileName(filePath);

            // Return the file name so it can be fetched later
            return Ok(new { fileName });
        }
        [HttpGet]
        [Route("get-file/{fileName}")]
        public async Task<IActionResult> GetFile(string fileName)
        {
            // Define custom temporary directory
            string tempDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tempFiles");

            // Ensure the directory exists
            if (!Directory.Exists(tempDirectory))
                Directory.CreateDirectory(tempDirectory);

            // Sanitize the file name to prevent path traversal
            if (string.IsNullOrEmpty(fileName) || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                return BadRequest("Invalid file name.");
            }

            // Combine and validate paths
            string fullPath = Path.Combine(tempDirectory, fileName);
            string resolvedFullPath = Path.GetFullPath(fullPath);

            if (!resolvedFullPath.StartsWith(tempDirectory, StringComparison.OrdinalIgnoreCase))
            {
                return Unauthorized("Access denied.");
            }

            // Check if the file exists
            if (!System.IO.File.Exists(resolvedFullPath))
            {
                return NotFound("File not found.");
            }

            // Determine content type
            string contentType = "application/octet-stream";
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (provider.TryGetContentType(resolvedFullPath, out var detectedContentType))
            {
                contentType = detectedContentType;
            }

            // Read the file into memory
            var memory = new MemoryStream();
            using (var stream = new FileStream(resolvedFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            System.IO.File.Delete(resolvedFullPath);
            // Return the file
            return File(memory, contentType, fileName);
        }
    }
}