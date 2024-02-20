using Domain.Entities;

namespace Application.DTOs.ChoiceDtos;

public class ResultChoiceDto
{
    public string ChoiceId { get; set; }
    public string ChoiceText { get; set; }
    public ChoiceType ChoiceType { get; set; }
}