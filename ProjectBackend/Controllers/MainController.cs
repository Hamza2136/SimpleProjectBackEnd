using Microsoft.AspNetCore.Mvc;
using ProjectBackend.Models;
using ProjectBackend.Services.Contract;

namespace ProjectBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : Controller
    {
        private readonly IMainService _mainService;
        public MainController(IMainService mainService)
        {
            _mainService = mainService;
        }


        [HttpGet("Login")]
        public IActionResult Login(string emailId, string password)
        {
            var user = _mainService.Login(emailId, password);

            if (user == null)
            {
                return Ok(new { message = "Invalid email or password", result = false });
            }

            return Ok(new { message = "Login successful", result = true, data = user });
        }


        [HttpGet("GetAll")]
        public IActionResult GetAllData()
        {
            var data = _mainService.GetData();
            return Ok(new { message = "Success", result = true, data });
        }

        [HttpGet("GetBySerialNumber")]
        public IActionResult GetDataBySerialNumber(int serialNumber)
        {
            var data = _mainService.GetDataBySerialNumber(serialNumber);
            if (data == null)
            {
                return NotFound(new { message = "Data not found", result = false });
            }
            return Ok(new { message = "Success", result = true, data });
        }

        [HttpPost("CreateData")]
        public IActionResult CreateData(MainModel model)
        {
            _mainService.SaveData(model);
            return Ok(new { message = "Data created successfully", result = true });
        }

        [HttpDelete("DeleteData")]
        public IActionResult DeleteData(int serialNumber)
        {
            var existingData = _mainService.GetDataBySerialNumber(serialNumber);
            if (existingData == null)
            {
                return NotFound(new { message = "Data not found", result = false });
            }
            _mainService.DeleteData(serialNumber);
            return Ok(new { message = "Data deleted successfully", result = true });
        }


        // Extra function Use if needed!
        [HttpPut("UpdateData")]
        public IActionResult UpdateData(int serialNumber, MainModel model)
        {
            var existingData = _mainService.GetDataBySerialNumber(serialNumber);
            if (existingData == null)
            {
                return NotFound(new { message = "Data not found", result = false });
            }
            _mainService.UpdateData(model);
            return Ok(new { message = "Data updated successfully", result = true });
        }

        // Upload Zip file Controller Code
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles(IFormFileCollection files, string uploadLink)
        {
            var result = await _mainService.UploadFilesAsync(files, uploadLink);

            if (!result)
            {
                return Ok(new { message = "File upload failed.", result = false });
            }

            return Ok(new { message = "Files uploaded successfully.", result = true });
        }
    }
}
