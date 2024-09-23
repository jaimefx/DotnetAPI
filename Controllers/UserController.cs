// using DotnetAPI.Data;
// using DotnetAPI.Dtos;
// using DotnetAPI.Models;
// using Microsoft.AspNetCore.Mvc;

// namespace DotnetAPI.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class UserController : ControllerBase
// {
//     private readonly DataContextDapper _dapper;

//     public UserController(IConfiguration config)
//     {
//         _dapper = new DataContextDapper(config);
//     }

//     [HttpGet("GetUsers/")]
//     public IEnumerable<User> GetUsers()
//     {
//         return _dapper.LoadData<User>("SELECT * FROM TutorialAppSchema.Users");
//     }

//     [HttpGet("GetSingleUser/{userId}")]
//     public User GetSingleUser(string userId)
//     {
//         return _dapper.LoadDataSingle<User>("SELECT * FROM TutorialAppSchema.Users WHERE UserId=" + userId.ToString());
//     }

//     [HttpPut(Name = "EditUser")]
//     public IActionResult EditUser(User user)
//     {
//         string sql = @"
//             UPDATE TutorialAppSchema.Users
//                 SET [FirstName] = '" + user.FirstName +
//                 "', [LastName] = '" + user.LastName +
//                 "', [Email] = '" + user.Email +
//                 "', [Gender] = '" + user.Gender +
//                 "', [Active] = '" + user.Active +
//                 "' WHERE UserId = " + user.UserId.ToString();

//         Console.WriteLine(sql);
//         if (_dapper.Execute(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to update user");
//     }

//     [HttpPost(Name = "AddUser")]
//     public IActionResult AddUser([FromBodyAttribute] UserToAddDTO user)
//     {
//         string sql = @"
//             INSERT INTO TutorialAppSchema.Users(
//                 [FirstName],
//                 [LastName],
//                 [Email],
//                 [Gender],
//                 [Active])
//                 VALUES ('" + user.FirstName + "','" + user.LastName + "','" + user.Email + "','" + user.Gender + "','" + user.Active + "')";

//         Console.WriteLine(sql);

//         if (_dapper.Execute(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to update user");
//     }

//     [HttpDelete(Name = "DeleteUser" )]
//     public IActionResult DeleteUser(int userId){
        
//         string sql = "DELETE FROM TutorialAppSchema.Users WHERE UserId = " + userId.ToString();

//         Console.WriteLine(sql);

//         if (_dapper.Execute(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to delete user");
//     }
// }