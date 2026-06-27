namespace LibraryManagement.Domain.Entities;

public class UserActivityLog : BaseEntity
{
    public int SystemUserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? Details { get; set; }
    public string? IpAddress { get; set; }

    // Navigation
    public SystemUser SystemUser { get; set; } = null!;
}