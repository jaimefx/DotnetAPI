namespace DotnetAPI.Dtos{
public partial class UserForLoginDTO{
        public string Email {get;set;}
        public string Password {get;set;}

        public UserForLoginDTO()
        {
            if(Email == null)
                Email = string.Empty;

            if(Password == null)
                Password = string.Empty;
        }
    }
}
