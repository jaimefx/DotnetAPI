using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoEFController : ControllerBase
{
    private IRepository<UserJobInfo> _userJobInfoRepository;

    public UserJobInfoEFController(IRepository<UserJobInfo> userJobInfoRepository)
    {
        _userJobInfoRepository = userJobInfoRepository;
    }

    [HttpGet]
    public IEnumerable<UserJobInfo> GetUsers()
    {
        return _userJobInfoRepository.GetAll();
    }

    [HttpGet("GetByUserId/{userId}")]
    public UserJobInfo GetByUserId(int userId)
    {
        return _userJobInfoRepository.GetSingle(userId);
    }

    [HttpPut]
    public IActionResult Edit(UserJobInfo userJobInfo)
    {
        UserJobInfo? usr = _userJobInfoRepository.GetSingle(userJobInfo.UserId);

        if (usr == null)
        {
            throw new Exception("Failed to get userJobInfo");
        }

        usr.Department = userJobInfo.Department;
        usr.JobTitle = userJobInfo.JobTitle;

        if (_userJobInfoRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("No update performed");
    }

    [HttpPost]
    public IActionResult AddUserSalary([FromBodyAttribute] UserJobInfo userJobInfo)
    {
        _userJobInfoRepository.AddEntity(userJobInfo);

        if (_userJobInfoRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Unable to add");
    }

    [HttpDelete]
    public IActionResult DeleteUser(int userId)
    {
        UserJobInfo? usr = _userJobInfoRepository.GetAll().Where(u => u.UserId == userId).FirstOrDefault();

        if (usr == null)
        {
            throw new Exception("Id does not match existing UserJobInfo");
        }

        _userJobInfoRepository.RemoveEntity(usr);

        if (_userJobInfoRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Failed to delete");
    }
}