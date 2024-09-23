using System.Runtime.CompilerServices;

namespace DotnetAPI.Models
{
    public partial class Post : UserIdentiy { 
        public int PostId {get;set;}
        public string PostTitle {get;set;}
        public string PostContent {get;set;}
        public DateTime PostCreated {get;set;}
        public DateTime PostUpdated {get;set;}

        public Post()
        {
            PostTitle ??= string.Empty;
            PostContent ??= string.Empty;
        }
    }
}