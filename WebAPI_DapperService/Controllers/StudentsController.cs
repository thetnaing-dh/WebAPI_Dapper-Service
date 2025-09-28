using Dapper.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_DapperService.Models.StudentModels;

namespace WebAPI_DapperService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly string _connectionString = "Server=.;Database=StudentDB;User Id=sa;Password=23032106;TrustServerCertificate=True;";
        private readonly DapperService _dapperService;
        public StudentsController()
        {
            _dapperService = new DapperService(_connectionString);
        }

        [HttpGet]
        public IActionResult Get()
        {
            string query = @"SELECT * FROM Tbl_Student WHERE DeleteFlag = 0;";
            List<StudentReqModel> StudentReqModels = _dapperService.Query<StudentReqModel>(query);
            if (StudentReqModels.Count == 0)
            {
                return NotFound("No Student Found.");
            }
            else
            {
                return Ok(StudentReqModels);
            }
        }

        [HttpPost]
        public IActionResult Insert(StudentReqModel StudentReqModel)
        {
            string query = @"INSERT INTO Tbl_Student(RollNo,Name,Email,DeleteFlag) 
                            VALUES(@RollNo, @Name, @Email,0);";

            var parameter = new StudentReqModel
            {
                RollNo = StudentReqModel.RollNo,
                Name = StudentReqModel.Name,
                Email = StudentReqModel.Email
            };

            var result = _dapperService.Execute(query, parameter);
            if (result > 0)
            {
                return Ok("Inserting Successfully.");
            }
            else
            {
                return BadRequest("Inserting Failed.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, StudentReqModel StudentReqModel)
        {
            string checkExistQuery = @"SELECT * FROM Tbl_Student WHERE deleteFlag = 0 AND Id = @Id;";

            var checkExist = _dapperService.QueryFirstOrDefault<StudentResModel>(checkExistQuery, new { Id = id });

            if (checkExist == null)
            {
                return BadRequest("Student Not Found or Deleted.");
            }

            string query = @"UPDATE Tbl_Student SET 
                            RollNo = @RollNo
                            , Name = @Name 
                            , Email = @Email                           
                            WHERE Id = @Id;";

            var parameter = new StudentResModel
            {
                Id = id,
                RollNo = StudentReqModel.RollNo,
                Name = StudentReqModel.Name,
                Email = StudentReqModel.Email
            };

            var result = _dapperService.Execute(query, parameter);

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
            string checkExistQuery = @"SELECT * FROM Tbl_Student WHERE deleteFlag = 0 AND Id = @Id;";

            var checkExist = _dapperService.QueryFirstOrDefault<StudentResModel>(checkExistQuery, new { Id = id });

            if (checkExist == null)
            {
                return BadRequest("Student Not Found or Deleted.");
            }

            string query = @"UPDATE Tbl_Student SET 
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