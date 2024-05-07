using ADO.NET_DataAdapter.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NET_DataAdapter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IServices _services;
        public StudentController(IServices services)
        {
            this._services = services;
        }
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok(_services.GetStudents());
        }
        [HttpPut]
        public IActionResult UpdateStudent(student student)
        {
            if (student != null) {
                _services.UpdateStudent(student);
                return Ok();
                    }
            return BadRequest();
        }
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            _services.DeleteStudent(id);
            return Ok();
        }
        [HttpPost]
        public IActionResult AddStudent(student student)
        {
            return Ok(_services.AddStuddent(student));
        }
    }
}
