namespace Application.DTOs.ExamAnswerDtos;

public class SubmitAnswerDto
{
    public Guid QuestionId { get; set; }
    public bool UserAnswer { get; set; }
}