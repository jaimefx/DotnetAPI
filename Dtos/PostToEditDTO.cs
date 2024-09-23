namespace DotnetAPI.Dtos{
    public partial class PostToEdit{
        public int PostId {get;set;}
        public string PostTitle {get;set;}
        public string PostContent {get;set;}

        public PostToEdit()
        {
            PostTitle ??= string.Empty;
            PostContent ??= string.Empty;
        }
    }
}