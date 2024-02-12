namespace Application.DTOs.ExamAnswerDtos;

public class UpdateExamAnswerDto
{
    public string Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ExamAssignmentId { get; set; }
    public Guid QuestionId { get; set; }
    public int? Score { get; set; }
    public int? SelectedChoiceId { get; set; }
    public DateTime AnsweredAt { get; set; }
    public Guid ExamId { get; set; }
}