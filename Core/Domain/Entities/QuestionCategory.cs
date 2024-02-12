using Domain.Entities.Common;

namespace Domain.Entities;

public class QuestionCategory: BaseEntity
{
    public string Name { get; set; }
    public Guid ExamId { get; set; }
    public virtual Exam Exam { get; set; }
    public virtual ICollection<Question> Questions { get; set; }
}