using Microsoft.IdentityModel.Tokens;

namespace DotnetAPI.Models
{
    public partial class UserJobInfo : UserIdentiy
    {
        public string JobTitle { get; set; }
        public string Department { get; set; }

        public UserJobInfo()
        {
            if (JobTitle == null)
                JobTitle = string.Empty;

            if (Department == null)
                Department = string.Empty;
        }
    }
}