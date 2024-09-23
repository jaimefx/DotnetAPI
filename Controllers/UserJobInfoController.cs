// using DotnetAPI.Data;
// using DotnetAPI.Dtos;
// using DotnetAPI.Models;
// using Microsoft.AspNetCore.Mvc;

// namespace DotnetAPI.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class UserSalaryController : ControllerBase
// {
//     private readonly DataContextDapper _dapper;

//     public UserSalaryController(IConfiguration config)
//     {
//         _dapper = new DataContextDapper(config);
//     }

//     [HttpGet()]
//     public IEnumerable<UserJobInfo> Get()
//     {
//         return _dapper.LoadData<UserJobInfo>("SELECT * FROM TutorialAppSchema.UserJobInfo");
//     }

//     [HttpGet("Get/{userId}")]
//     public UserSalary GetByUserId(string userId)
//     {
//         return _dapper.LoadDataSingle<UserSalary>("SELECT * FROM TutorialAppSchema.UserJobInfo WHERE UserId=" + userId.ToString());
//     }

//     [HttpPut]
//     public IActionResult EditJobInfo(UserJobInfo userJobInfo)
//     {
//         string sql = @"
//             UPDATE TutorialAppSchema.UserJobInfo
//                 SET [JobTitle] = '" + userJobInfo.JobTitle +
//                 "', [Department] = '" + userJobInfo.Department +
//                 "' WHERE UserId = " + userJobInfo.UserId.ToString();

//         Console.WriteLine(sql);
//         if (_dapper.Execute(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to update userJobInfo");
//     }

//     [HttpPost]
//     public IActionResult Add([FromBodyAttribute] UserJobInfo userJobInfo)
//     {
//         string sql = @"
//             INSERT INTO TutorialAppSchema.UserJobInfo(
//                 [UserId],
//                 [JobTitle],
//                 [Department])
//                 VALUES ('" + userJobInfo.UserId + "','" + userJobInfo.JobTitle + "','" + userJobInfo.Department + "')";

//         Console.WriteLine(sql);

//         if (_dapper.Execute(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to update userJobInfo");
//     }

//     [HttpDelete]
//     public IActionResult DeleteUser(int userId){
        
//         string sql = "DELETE FROM TutorialAppSchema.UserJobInfo WHERE UserId = " + userId.ToString();

//         Console.WriteLine(sql);

//         if (_dapper.Execute(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to delete UserJobInfo");
//     }
// }