﻿using Microsoft.EntityFrameworkCore;
using Social_Media.Models;
using System.Collection.Generic;
using System.Linq;

namespace Social_Media.Services;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(user.UserID))
            {
                return null;
            }
            else
            {
                throw;
            }
        }

        return user;
    }

    public async Task<User> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return null;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return user;
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.UserID == id);
    }
    public List<User> GetUsersSortedByEngagement(List<User> users){
        return users.OrderByDescending(u => u.CalculateEngagementScore).ToList();
    }
    public void DisplayEngagementScores(List<User> users)
    {
        // Calculate and sort users by EngagementScore in descending order
        var sortedUsers = users.OrderByDescending(u => u.EngagementScore).ToList();

        Console.WriteLine("Engagement Scores:");
        foreach (var user in sortedUsers)
        {
            Console.WriteLine($"User: {user.UserName}, Score: {user.EngagementScore}");
        }
    }
}