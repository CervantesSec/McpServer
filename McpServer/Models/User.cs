namespace McpServer.Models;

public class ApplicationUser
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? NormalizedUserName { get; set; }
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PasswordHash { get; set; }
    public string? SecurityStamp { get; set; }
    public string? ConcurrencyStamp { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public string? FullName { get; set; }
    public string? Avatar { get; set; }
    public string? Description { get; set; }
    public string? Position { get; set; }
    public Guid? ClientId { get; set; }
    public bool ExternalLogin { get; set; }
    public List<ChatMessage>? ChatMessagesFromUsers { get; set; }
    public List<ChatMessage>? ChatMessagesToUsers { get; set; }
}

public class UserCreateViewModel
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }
    public string? Position { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Role { get; set; }
    public string? FileName { get; set; }
    public byte[]? FileContent { get; set; }
    public bool ExternalLogin { get; set; }
    public Guid? ClientId { get; set; }
}

public class UserEditViewModel
{
    public string? Id { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Description { get; set; }
    public string? Position { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Role { get; set; }
    public string? FileName { get; set; }
    public byte[]? FileContent { get; set; }
    public Guid? ClientId { get; set; }
    public string? ImagePath { get; set; }
    public bool ExternalLogin { get; set; }
    public bool Lockout { get; set; }
    public bool TwoFactorEnabled { get; set; }
}

// Placeholder for ChatMessage - this is referenced in ApplicationUser but not defined in our models
public class ChatMessage
{
    public Guid Id { get; set; }
    public string? Message { get; set; }
    public string? FromUserId { get; set; }
    public string? ToUserId { get; set; }
    public DateTime CreatedDate { get; set; }
}