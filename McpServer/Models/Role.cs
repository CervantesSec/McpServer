namespace McpServer.Models;

public enum RoleTypes
{
    BasicRole = 0,
    ManagementRole = 50,
    AdminRole = 60,
    SuperAdminRole = 100
}

public class RoleWithPermissionNamesDto
{
    public string RoleName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public RoleTypes RoleType { get; set; }
    public string PackedPermissionsInRole { get; set; } = string.Empty;
    public List<string>? PermissionNames { get; set; }
}

public class CreateRoleViewModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<string>? Permissions { get; set; }
}