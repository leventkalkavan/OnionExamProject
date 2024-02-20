using Domain.Entities.Common;

namespace Domain.Entities;

public class Exam : BaseEntity
{
    public string Name { get; set; }
    public int ExamMinute { get; set; }
    public string Description { get; set; }
    public int SuccessScore { get; set; }
    public Guid ExamScenarioCategoryId { get; set; }
    public virtual ExamScenarioCategory ExamScenarioCategory { get; set; }
    public virtual ICollection<QuestionCategory> QuestionCategories { get; set; }
}