using Microsoft.AspNetCore.Mvc;
using Social_Media.Models;
using Social_Media.Services;
using System.Collections.Generic;

namespace Social_Media.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.UserID }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.UserID)
        {
            return BadRequest();
        }

        var result = await _userService.UpdateUserAsync(user);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUserAsync(id);

        if (result == null)
        {
            return NotFound();
        }

        return NoContent();
    }
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService(); // Ideally, use dependency injection.
        }

        public IActionResult Index()
        {
            // Sample users data (in real projects, this would come from a database)
            var users = new List<User>
            {
                new User { UserID = 1, UserName = "Alice", Posts = 10, Likes = 20, Comments = 15 },
                new User { UserID = 2, UserName = "Bob", Posts = 5, Likes = 30, Comments = 10 },
                new User { UserID = 3, UserName = "Charlie", Posts = 8, Likes = 25, Comments = 20 }
            };

            // Get users sorted by engagement score
            var sortedUsers = _userService.GetUsersSortedByEngagement(users);

            // Pass the sorted list to the view
            return View(sortedUsers);
        }
    }
}