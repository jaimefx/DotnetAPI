using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSalaryEFController : ControllerBase
{
    private IRepository<UserSalary> _userSalaryRepository;

    public UserSalaryEFController(IRepository<UserSalary> userSalaryRepository)
    {
        _userSalaryRepository = userSalaryRepository;
    }

    [HttpGet]
    public IEnumerable<UserSalary> GetUsers()
    {
        return _userSalaryRepository.GetAll();
    }

    [HttpGet("Get/{userId}")]
    public UserSalary GetByUserId(int userId)
    {
        return _userSalaryRepository.GetSingle(userId);
    }

    [HttpPut]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        UserSalary? usr = _userSalaryRepository.GetSingle(userSalary.UserId);

        if (usr == null)
        {
            throw new Exception("Failed to get userSalary");
        }

        usr.Salary = userSalary.Salary;
        usr.AvgSalary = userSalary.AvgSalary;

        if (_userSalaryRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("No update performed");
    }

    [HttpPost]
    public IActionResult AddUserSalary([FromBodyAttribute] UserSalary userSalary)
    {
        _userSalaryRepository.AddEntity(userSalary);

        if (_userSalaryRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Unable to add");
    }

    [HttpDelete]
    public IActionResult DeleteUser(int userId)
    {
        UserSalary? usr = _userSalaryRepository.GetSingle(userId);

        if (usr == null)
        {
            throw new Exception("Id does not match existing UserSalary");
        }

        _userSalaryRepository.RemoveEntity(usr);

        if (_userSalaryRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Failed to delete");
    }
}