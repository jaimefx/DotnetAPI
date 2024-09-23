using System.Text.Json;
using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class PostController : ControllerBase
    {
        private IRepository<Post> _postRepo;
        private IMapper mapper;


        public PostController(IRepository<Post> postRepo)
        {
            _postRepo = postRepo;
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PostToAddDTO, Post>();
                cfg.CreateMap<PostToEdit, Post>();
            }));

        }

        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            return _postRepo.GetAll();
        }

        [HttpGet("PostSingle/{postId}")]
        public Post? GetPostSingle(int postId)
        {
            return _postRepo.GetAll().FirstOrDefault(post => post.PostId == postId);
        }

        [HttpGet("PostsByUser/{userId}")]
        public IEnumerable<Post> GetPostsByUser(int userId)
        {
            return _postRepo.GetAll().Where(post => post.UserId == userId);
        }

        [HttpGet("MyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            string? userIdFromToken = User.FindFirst("userId")?.Value;
            if (userIdFromToken == null)
            {
                return (IEnumerable<Post>)StatusCode(403, "userId does not exist");
            }

            int userId = int.Parse(userIdFromToken);
            return _postRepo.GetAll().Where(post => post.UserId == userId);
        }

        [HttpPost]
        public IActionResult AddPost(PostToAddDTO postToAdd)
        {
            string? userFromToken = User.FindFirst("UserId")?.Value;

            if (userFromToken == null)
            {
                return StatusCode(403, "no user identified");
            }

            Post post = mapper.Map<Post>(postToAdd);
            post.PostCreated = DateTime.Now;
            post.PostUpdated = DateTime.Now;
            post.UserId = int.Parse(userFromToken);

            Console.WriteLine(JsonSerializer.Serialize(post));
            _postRepo.AddEntity(post);

            if (_postRepo.SaveChanges())
            {
                return Ok();
            }

            return StatusCode(400, "Failed to create post");
        }

        [HttpPut]
        public IActionResult EditPost(PostToEdit postToEdit)
        {
            Post? post = _postRepo.GetAll().Where(post => (post.PostId == postToEdit.PostId) && post.UserId.ToString().Equals(User.FindFirst("userId")?.Value)).FirstOrDefault();

            if (post != null)
            {
                post.PostTitle = postToEdit.PostTitle;
                post.PostContent = postToEdit.PostContent;

                if (_postRepo.SaveChanges())
                {
                    return Ok();
                }

                throw new Exception("Failed to edit post!");
            }

            return StatusCode(404, "post does not exist");
        }

        [HttpDelete]
        public IActionResult DeletePost(int postId)
        {
            Post? post = _postRepo.GetAll().Where(p => p.PostId == postId && p.UserId.ToString().Equals(User.FindFirst("UserId")?.Value)).FirstOrDefault();

            if (post != null)
            {
                _postRepo.RemoveEntity(post);

                if (_postRepo.SaveChanges())
                {
                    return Ok();
                }

                throw new Exception("Failed to delete post!");
            }
            return StatusCode(404, "post does not exist");
        }

        [HttpGet("PostsBySearch/{searchParam}")]
        public IEnumerable<Post>  PostsBySearch(string searchParam)
        {
            IEnumerable<Post> posts = _postRepo.GetAll().Where(p => p.PostTitle.ToLower().Contains(searchParam.ToLower()) || p.PostContent.ToLower().Contains(searchParam.ToLower()));
            return posts;
        }
    }
}