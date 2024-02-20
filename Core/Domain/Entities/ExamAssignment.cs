using Domain.Entities.Common;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class ExamAssignment: BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ExamId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public virtual Exam Exam { get; set; }
}