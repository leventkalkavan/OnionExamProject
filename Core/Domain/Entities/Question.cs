using Domain.Entities.Common;

namespace Domain.Entities;

public class Question: BaseEntity
{
    public string Description { get; set; }
    public Guid QuestionCategoryId { get; set; }
    public virtual QuestionCategory QuestionCategory { get; set; }
    public virtual ICollection<Choice> Choices { get; set; }
}