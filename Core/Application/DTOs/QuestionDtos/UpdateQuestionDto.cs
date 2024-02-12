namespace Application.DTOs.QuestionDtos;

public class UpdateQuestionDto
{
    public string Id { get; set; }
    public string Description { get; set; }
    public Guid QuestionCategoryId { get; set; }
}