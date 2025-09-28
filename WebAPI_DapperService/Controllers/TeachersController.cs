using Dapper.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using WebAPI_DapperService.Models.TeacherModels;

namespace WebAPI_DapperService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly string _connectionString = "Server=.;Database=TeacherDB;User Id=sa;Password=23032106;TrustServerCertificate=True;";
        private readonly DapperService _dapperService;

        public TeachersController() 
        {
            _dapperService = new DapperService(_connectionString);
        }

        [HttpGet]
        public IActionResult Get()
        {
            string query = @"SELECT * FROM Tbl_Teacher WHERE DeleteFlag = 0;";
            List<TeacherReqModel> teacherReqModels = _dapperService.Query<TeacherReqModel>(query);
            if (teacherReqModels.Count == 0)
            {
                return NotFound("No Teacher Found.");
            }
            else
            {
                return Ok(teacherReqModels);
            }
        }

        [HttpPost]
        public IActionResult Insert(TeacherResModel teacherResModel)
        {
            string query = $@"INSERT INTO Tbl_Teacher(Name,Phone,Subject,DeleteFlag) 
                            VALUES('{teacherResModel.Name}' 
                            ,'{teacherResModel.Phone}' 
                            ,'{teacherResModel.Subject}',0);";
            var result = _dapperService.Execute(query);
            if(result > 0)
            {
                return Ok("Inserting Successfully.");
            }
            else
            {
                return BadRequest("Inserting Failed.");
            }           
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TeacherResModel teacherResModel)
        {
            string checkExistQuery = @"SELECT * FROM Tbl_Teacher WHERE deleteFlag = 0 AND Id = @Id;";                    

            var checkExist = _dapperService.QueryFirstOrDefault<TeacherResModel>(checkExistQuery, new { Id = id });

            if (checkExist == null)
            {
                return BadRequest("Teacher Not Found or Deleted.");
            }

            string query = @"UPDATE Tbl_Teacher SET 
                            Name = @Name 
                            , Phone = @Phone 
                            , Subject = @Subject 
                            WHERE Id = @Id;";

            var result = _dapperService.Execute(query, new
            {
                Id = id,
                Name = teacherResModel.Name,
                Phone = teacherResModel.Phone,
                Subject = teacherResModel.Subject
            });

            if (result > 0)
            {
                return Ok("Updating Successfully.");
            }
            else
            {
                return BadRequest("Updating Failed.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string checkExistQuery = @"SELECT * FROM Tbl_Teacher WHERE deleteFlag = 0 AND Id = @Id;";

            var checkExist = _dapperService.QueryFirstOrDefault<TeacherResModel>(checkExistQuery, new { Id = id });

            if (checkExist == null)
            {
                return BadRequest("Teacher Not Found or Deleted.");
            }

            string query = @"UPDATE Tbl_Teacher SET 
                           DeleteFlag = 1 
                           WHERE Id = @Id;";

            var result = _dapperService.Execute(query, new { Id = id });

            if (result > 0)
            {
                return Ok("Deleting Successfully.");
            }
            else
            {
                return BadRequest("Deleting Failed.");
            }
        }
    }
}