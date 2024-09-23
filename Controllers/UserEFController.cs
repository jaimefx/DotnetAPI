using System.Text.Json;
using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    private IRepository<User> _userRepository;
    private IMapper mapper;

    public UserEFController(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
        mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.CreateMap<UserToAddDTO, User>();
        }));
    }

    [HttpGet("GetUsers/")]
    public IEnumerable<User> GetUsers()
    {
        return _userRepository.GetAll();
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        return _userRepository.GetSingle(userId);
    }

    [HttpPut(Name = "EditUserEF")]
    public IActionResult EditUser(User user)
    {
        User? usr = _userRepository.GetSingle(user.UserId);

        if (usr == null)
        {
            throw new Exception("Failed to get user");
        }

        usr.Active = user.Active;
        usr.Email = user.Email;
        usr.FirstName = user.FirstName;
        usr.LastName = user.LastName;
        usr.Gender = user.Gender;

        if (_userRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("No update performed");
    }

    [HttpPost(Name = "AddUserEF")]
    public IActionResult AddUser([FromBodyAttribute] UserToAddDTO user)
    {
        User usr = mapper.Map<User>(user);
        Console.WriteLine(JsonSerializer.Serialize(usr));
        _userRepository.AddEntity(usr);

        if (_userRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Unable to add");
    }

    [HttpDelete(Name = "DeleteUserEF")]
    public IActionResult DeleteUser(int userId)
    {
        User? usr = _userRepository.GetSingle(userId);

        if (usr == null)
        {
            throw new Exception("Id does not match existing user");
        }

        _userRepository.RemoveEntity(usr);

        if (_userRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Failed to delete");
    }
}