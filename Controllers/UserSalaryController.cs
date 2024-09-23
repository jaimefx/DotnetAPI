// using DotnetAPI.Data;
// using DotnetAPI.Dtos;
// using DotnetAPI.Models;
// using Microsoft.AspNetCore.Mvc;

// namespace DotnetAPI.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class UserJobInfoController : ControllerBase
// {
//     private readonly DataContextDapper _dapper;

//     public UserJobInfoController(IConfiguration config)
//     {
//         _dapper = new DataContextDapper(config);
//     }

//     [HttpGet()]
//     public IEnumerable<UserSalary> GetUsers()
//     {
//         return _dapper.LoadData<UserSalary>("SELECT * FROM TutorialAppSchema.UserSalary");
//     }

//     [HttpGet("Get/{userId}")]
//     public UserSalary GetSingleUser(string userId)
//     {
//         return _dapper.LoadDataSingle<UserSalary>("SELECT * FROM TutorialAppSchema.UserSalary WHERE UserId=" + userId.ToString());
//     }

//     [HttpPut]
//     public IActionResult EditUser(UserSalary user)
//     {
//         string sql = @"
//             UPDATE TutorialAppSchema.UserSalary
//                 SET [Salary] = '" + user.Salary +
//                 "', [AvgSalary] = '" + user.AvgSalary +
//                 "' WHERE UserId = " + user.UserId.ToString();

//         Console.WriteLine(sql);
//         if (_dapper.Execute(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to update UserSalary");
//     }

//     [HttpPost]
//     public IActionResult AddUser([FromBodyAttribute] UserSalary user)
//     {
//         string sql = @"
//             INSERT INTO TutorialAppSchema.UserSalary(
//                 [UserId],
//                 [Salary],
//                 [AvgSalary])
//                 VALUES ('" + user.UserId + "','" + user.Salary + "','" + user.AvgSalary + "')";

//         Console.WriteLine(sql);

//         if (_dapper.Execute(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to update UserSalary");
//     }

//     [HttpDelete]
//     public IActionResult DeleteUser(int userId){
        
//         string sql = "DELETE FROM TutorialAppSchema.UserSalary WHERE UserId = " + userId.ToString();

//         Console.WriteLine(sql);

//         if (_dapper.Execute(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to delete UserSalary");
//     }
// }