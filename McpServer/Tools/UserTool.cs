using McpServer.Models;
using McpServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace McpServer.Tools;

[McpServerToolType]
public class UserTool
{
    private readonly CervantesApiClient _apiClient;
    private readonly ILogger<UserTool> _logger;

    public UserTool(CervantesApiClient apiClient, ILogger<UserTool> logger)
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    [McpServerTool, Description("Get all users")]
    public async Task<List<ApplicationUser>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching all users");
        var users = await _apiClient.GetAsync<List<ApplicationUser>>("api/User", cancellationToken);
        return users ?? new List<ApplicationUser>();
    }

    [McpServerTool, Description("Get a specific user by ID")]
    public async Task<ApplicationUser?> GetUserByIdAsync(
        [Description("User ID")] string userId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching user with ID: {UserId}", userId);
        return await _apiClient.GetAsync<ApplicationUser>($"api/User/{userId}", cancellationToken);
    }

    [McpServerTool, Description("Create a new user")]
    public async Task<ApplicationUser?> CreateUserAsync(
        [Description("Full name of the user")] string fullName,
        [Description("Email address")] string email,
        [Description("Password")] string password,
        [Description("Confirm password")] string confirmPassword,
        [Description("User role")] string role,
        [Description("Phone number")] string? phoneNumber = null,
        [Description("Description")] string? description = null,
        [Description("Position/Job title")] string? position = null,
        [Description("Client ID")] Guid? clientId = null,
        [Description("Is external login")] bool externalLogin = false,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new user: {UserEmail}", email);
        
        var userData = new UserCreateViewModel
        {
            FullName = fullName,
            Email = email,
            Password = password,
            ConfirmPassword = confirmPassword,
            Role = role,
            PhoneNumber = phoneNumber,
            Description = description,
            Position = position,
            ClientId = clientId,
            ExternalLogin = externalLogin
        };

        return await _apiClient.PostAsync<UserCreateViewModel, ApplicationUser>("api/User", userData, cancellationToken);
    }

    [McpServerTool, Description("Update an existing user")]
    public async Task<ApplicationUser?> UpdateUserAsync(
        [Description("User ID")] string id,
        [Description("Full name of the user")] string fullName,
        [Description("Email address")] string email,
        [Description("User role")] string role,
        [Description("Phone number")] string? phoneNumber = null,
        [Description("Description")] string? description = null,
        [Description("Position/Job title")] string? position = null,
        [Description("Password (leave empty to keep current)")] string? password = null,
        [Description("Confirm password")] string? confirmPassword = null,
        [Description("Client ID")] Guid? clientId = null,
        [Description("Is external login")] bool externalLogin = false,
        [Description("Account is locked out")] bool lockout = false,
        [Description("Two-factor authentication enabled")] bool twoFactorEnabled = false,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating user: {UserId}", id);
        
        var userData = new UserEditViewModel
        {
            Id = id,
            FullName = fullName,
            Email = email,
            Role = role,
            PhoneNumber = phoneNumber,
            Description = description,
            Position = position,
            Password = password,
            ConfirmPassword = confirmPassword,
            ClientId = clientId,
            ExternalLogin = externalLogin,
            Lockout = lockout,
            TwoFactorEnabled = twoFactorEnabled
        };

        return await _apiClient.PutAsync<UserEditViewModel, ApplicationUser>("api/User", userData, cancellationToken);
    }

    [McpServerTool, Description("Get user role by user ID")]
    public async Task<string?> GetUserRoleAsync(
        [Description("User ID")] string userId,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching role for user: {UserId}", userId);
        return await _apiClient.GetAsync<string>($"api/User/Role?userId={userId}", cancellationToken);
    }

    [McpServerTool, Description("Update user avatar")]
    public async Task<bool> UpdateUserAvatarAsync(
        [Description("Avatar file content as base64 string")] string avatarContent,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating user avatar");
        var data = new { avatarContent };
        return await _apiClient.PostAsync("api/User/Avatar", data, cancellationToken);
    }

    [McpServerTool, Description("Update user profile")]
    public async Task<bool> UpdateUserProfileAsync(
        [Description("Profile data as JSON")] string profileData,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating user profile");
        return await _apiClient.PutAsync("api/User/Profile", profileData, cancellationToken);
    }
}