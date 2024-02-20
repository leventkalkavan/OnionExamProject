using Application.DTOs.ChoiceDtos;

namespace Application.DTOs.QuestionDtos;

public class ResultQuestionDto
{
    public Guid QuestionId { get; set; }
    public string Description { get; set; }
    public List<ResultChoiceDto> Choices { get; set; }
}