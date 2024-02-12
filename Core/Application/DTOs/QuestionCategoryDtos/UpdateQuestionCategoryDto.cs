namespace Application.DTOs.QuestionCategoryDtos;

public class UpdateQuestionCategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Guid ExamId { get; set; }
}