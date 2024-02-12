using Domain.Entities.Common;

namespace Domain.Entities;

public class Choice: BaseEntity
{
    public string Text { get; set; }
    public ChoiceType ChoiceType { get; set; }
    public Guid QuestionId { get; set; }
    public virtual Question Question { get; set; }
}