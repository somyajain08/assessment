using Social_Media.Models;
using Social_Media.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace Social_Media.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
    {
        var posts = await _postService.GetPostsAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(Post post)
    {
        await _postService.CreatePostAsync(post);
        return CreatedAtAction(nameof(GetPost), new { id = post.PostID }, post);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, Post post)
    {
        if (id != post.PostID)
        {
            return BadRequest();
        }

        var result = await _postService.UpdatePostAsync(post);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var result = await _postService.DeletePostAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
public class PostController : Controller
    {
        private readonly ContentModerationService _moderationService;

        public PostController()
        {
            _moderationService = new ContentModerationService();
        }

        // Display form to create a post
        public IActionResult Create()
        {
            return View(new Post());
        }

        // Handle form submission
        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (!_moderationService.IsContentValid(post.Content, out string feedbackMessage))
            {
                // If content is invalid, show feedback message
                post.IsFlagged = true;
                post.FlagMessage = feedbackMessage;
                return View(post);  // Return the same view with feedback
            }

            // If valid, save the post (for simplicity, just return success view)
            return RedirectToAction("Success");
        }

        // Display a success message after posting
        public IActionResult Success()
        {
            return View();
        }
    }