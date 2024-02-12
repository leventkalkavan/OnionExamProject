namespace Application.DTOs.QuestionDtos;

public class CreateQuestionDto
{
    public string Description { get; set; }
    public Guid QuestionCategoryId { get; set; }
}