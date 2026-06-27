using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Domain.Entities;

public class SystemUser : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }

    // Navigation
    public ICollection<UserActivityLog> ActivityLogs { get; set; } = new List<UserActivityLog>();
}