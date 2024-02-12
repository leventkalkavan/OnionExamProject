using Domain.Entities;

namespace Application.DTOs.ChoiceDtos;

public class CreateChoiceDto
{
    public string Text { get; set; }
    public ChoiceType ChoiceType { get; set; }
    public Guid QuestionId { get; set; }
}