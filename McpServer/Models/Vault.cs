namespace McpServer.Models;

public enum VaultType
{
    Credential = 0,
    Note = 1,
    Identity = 2,
    Card = 3,
    SecureNote = 4,
    Other = 5
}

public class Vault
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string? Name { get; set; }
    public VaultType Type { get; set; }
    public string? Description { get; set; }
    public string? Value { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? UserId { get; set; }
}

public class VaultCreateViewModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public VaultType Type { get; set; }
    public string? Value { get; set; }
    public Guid ProjectId { get; set; }
}

public class VaultEditViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public VaultType Type { get; set; }
    public string? Value { get; set; }
    public Guid ProjectId { get; set; }
}