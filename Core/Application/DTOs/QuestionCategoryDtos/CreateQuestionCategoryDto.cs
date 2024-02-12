namespace Application.DTOs.QuestionCategoryDtos;

public class CreateQuestionCategoryDto
{
    public string Name { get; set; }
    public Guid ExamId { get; set; }
}