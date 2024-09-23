using Microsoft.IdentityModel.Tokens;

namespace DotnetAPI.Models
{
    public partial class UserSalaryDTO
    {
        public decimal Salary { get; set; }
        public decimal AvgSalary { get; set; }

    }
}