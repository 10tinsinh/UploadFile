using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UploadFile.Model;
using UploadFile.Service;

namespace UploadFile.Controllers
{
    
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _student;

        public StudentController(IStudentService student)
        {
            _student = student;
        }

        [HttpGet]
        [Route("/api/GetData")]
        public async Task<IActionResult> GetData()
        {
            try
            {
                var data = await _student.GetData();
                return Ok(new List<StudentNoIdModel>(data));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("/api/CreateData")]
        public async Task<IActionResult> CreateData(StudentNoIdModel student)
        {
            try
            {
                var data = new StudentNoIdModel(student);
                var result = await _student.Create(data);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("/api/export")]
        public async Task<IActionResult> ExportExcel(string code)
        {
            try
            {
                
                var result = await _student.ExportExcel(code);
                if(result.Name != "")
                {
                    return Ok (new Response 
                    {
                        Success = result.LinkFile,
                        Message = "Export Successfully"
                    }
                    );
                }
                return BadRequest();
                
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("/api/import")]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            try
            {

                var result = await _student.ImportExcel(file);
                return Ok(new Response
                {
                    Success = "",
                    Message = result
                }
                );
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/delete")]
        public async Task<IActionResult> DeleteData(string code)
        {
            try
            {

                await _student.Delete(code);
                return Ok(new Response
                {
                    Success = "True",
                    Message = "Export Successfully"
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
