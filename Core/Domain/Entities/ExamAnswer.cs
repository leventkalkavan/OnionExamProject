using Domain.Entities.Common;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class ExamAnswer : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ExamAssignmentId { get; set; }
    public int Score { get; set; }
    public DateTime AnsweredAt { get; set; }
    public bool UserAnswer { get; set; }
    public Guid QuestionId { get; set; }
    public virtual Question Question { get; set; }
    public virtual ExamAssignment ExamAssignment { get; set; }
}