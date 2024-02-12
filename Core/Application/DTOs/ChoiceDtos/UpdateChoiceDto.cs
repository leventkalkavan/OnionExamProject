using Domain.Entities;

namespace Application.DTOs.ChoiceDtos;

public class UpdateChoiceDto
{
    public string Id { get; set; }
    public string Text { get; set; }
    public ChoiceType ChoiceType { get; set; }
    public Guid QuestionId { get; set; }
}