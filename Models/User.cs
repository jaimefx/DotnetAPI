using Microsoft.IdentityModel.Tokens;

namespace DotnetAPI.Models
{
    public partial class User : UserIdentiy
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public bool Active { get; set; }

        public User()
        {
            if (FirstName == null)
                FirstName = string.Empty;

            if (LastName == null)
                LastName = string.Empty;

            if (Email == null)
                Email = string.Empty;

            if (Gender == null)
                Gender = string.Empty;
        }
    }
}