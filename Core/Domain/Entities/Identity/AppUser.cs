using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class AppUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenTime { get; set; }
    public UserRole Role { get; set; }
    public virtual ICollection<ExamAssignment> ExamAssignments { get; set; }
}