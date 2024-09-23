using Microsoft.IdentityModel.Tokens;

namespace DotnetAPI.Models
{
public partial class UserSalary : UserIdentiy
    {
        public decimal Salary { get; set; }
        public decimal AvgSalary { get; set; }

    }
}