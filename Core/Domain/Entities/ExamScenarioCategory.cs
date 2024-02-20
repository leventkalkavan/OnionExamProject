using Domain.Entities.Common;

namespace Domain.Entities;

public class ExamScenarioCategory: BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Exam > Exams { get; set; } 
}