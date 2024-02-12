using Domain.Entities.Common;

namespace Domain.Entities;

public class Exam: BaseEntity
{
    public string Name { get; set; }
    public int ExamMinute { get; set; }
    public string Description { get; set; }
    public int SuccessScore { get; set; }
    public Guid ScenarioCategoryId { get; set; }
    public virtual ScenarioCategory ScenarioCategory { get; set; }
    public virtual ICollection<QuestionCategory> QuestionCategories { get; set; }
    public virtual ICollection<ExamAssignment> ExamAssignments { get; set; }
}