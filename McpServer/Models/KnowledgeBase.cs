namespace McpServer.Models;

public class KnowledgeBase
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Order { get; set; }
    public string? CreatedUserId { get; set; }
    public string? UpdatedUserId { get; set; }
    public Guid CategoryId { get; set; }
    public List<KnowledgeBaseTags>? Tags { get; set; }
}

public class KnowledgePageCreateViewModel
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int Order { get; set; }
    public Guid CategoryId { get; set; }
}

public class KnowledgePageEditViewModel
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public int Order { get; set; }
    public Guid CategoryId { get; set; }
}

public class KnowledgeBaseCategories
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public int Order { get; set; }
    public string? UserId { get; set; }
    public List<KnowledgeBase>? Pages { get; set; }
}

public class KnowledgeBaseCategoryCreateViewModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public int Order { get; set; }
}

public class KnowledgeBaseCategoryEditViewModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public int Order { get; set; }
}

public class KnowledgeBaseTags
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<KnowledgeBase>? Notes { get; set; }
}