using Domain.Entities.Common;

namespace Domain.Entities;

public class ScenarioCategory: BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Exam > Exams { get; set; } 
}