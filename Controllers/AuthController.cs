using System.Security.Cryptography;
using System.Text.Json;
using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Helpers;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DotnetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        private IRepository<User> _userRepo;
        private IMapper mapper;
        private readonly AuthHelper _authHelper;

        public AuthController(IConfiguration config, IRepository<User> userRepo)
        {
            _dapper = new DataContextDapper(config);
            _userRepo = userRepo;
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserForRegistrationDTO, User>();
            }));
            _authHelper = new AuthHelper(config);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserForRegistrationDTO userForRegistrationDTO)
        {
            if (userForRegistrationDTO.Password.Equals(userForRegistrationDTO.PasswordConfirm))
            {
                string sql = "SELECT * FROM TutorialAppSchema.Auth WHERE Email = '"
                    + userForRegistrationDTO.Email + "'";

                IEnumerable<string> existingUsers = _dapper.LoadData<string>(sql);

                if (existingUsers.Count() == 0)
                {
                    byte[] passwordSalt = new byte[128 / 8];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetNonZeroBytes(passwordSalt);
                    }

                    byte[] passwordHash = _authHelper.GetPasswordHash(userForRegistrationDTO.Password, passwordSalt);

                    string sqlAddAuth = @"
                        INSERT INTO TutorialAppSchema.Auth (
                        [Email],
                        [PasswordHash],
                        [PasswordSalt]) VALUES ('" + userForRegistrationDTO.Email
                        + "', @PasswordHash, @PasswordSalt)";

                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    SqlParameter passWordSaltParam = new SqlParameter("@PasswordSalt", System.Data.SqlDbType.VarBinary);
                    passWordSaltParam.Value = passwordSalt;
                    SqlParameter passWordHashParam = new SqlParameter("@PasswordHash", System.Data.SqlDbType.VarBinary);
                    passWordHashParam.Value = passwordHash;

                    sqlParameters.Add(passWordSaltParam);
                    sqlParameters.Add(passWordHashParam);

                    if (_dapper.ExecuteSqlWithParams(sqlAddAuth, sqlParameters))
                    {
                        User usr = mapper.Map<User>(userForRegistrationDTO);
                        usr.Active = true;
                        Console.WriteLine(JsonSerializer.Serialize(usr));
                        _userRepo.AddEntity(usr);
                        if (_userRepo.SaveChanges())
                        {
                            return Ok();
                        }
                        throw new Exception("Failed to save user");

                    }
                    throw new Exception("Failed to register user");

                }
                throw new Exception("User with this email already exists");

            }

            throw new Exception("Passwords do not match");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDTO userForLoginDTO)
        {
            string sqlForHashAndSalt = @"
                SELECT [PasswordHash],
                [PasswordSalt] FROM TutorialAppSchema.Auth WHERE Email = '" + userForLoginDTO.Email + "'";

            UserForLoginConfirmationDTO userForConfirmation = _dapper.LoadDataSingle<UserForLoginConfirmationDTO>(sqlForHashAndSalt);

            byte[] passwordHash = _authHelper.GetPasswordHash(userForLoginDTO.Password, userForConfirmation.PasswordSalt);

            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != userForConfirmation.PasswordHash[i])
                {
                    return StatusCode(401, "Incorrect password!");
                }
            }

            User? user = _userRepo.GetAll().Where(usr => usr.Email.Equals(userForLoginDTO.Email)).FirstOrDefault();

            if (user != null)
            {
                return Ok(new Dictionary<string, string>(){
                    {"token", _authHelper.CreateToken(user.UserId)}
                });
            }

            return StatusCode(401, "unable to identify");
        }

        [HttpGet("RefreshToken")]
        public IActionResult RefreshToken()
        {
            int userId = Int32.Parse(User.FindFirst("userId")?.Value + string.Empty);

            int userFromDB = _userRepo.GetSingle(userId).UserId;

            return Ok(new Dictionary<string, string>(){
                {"token", _authHelper.CreateToken(userFromDB)}
            });
        }
    }
}