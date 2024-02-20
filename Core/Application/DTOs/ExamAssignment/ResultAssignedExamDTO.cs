namespace Application.DTOs.ExamAssignment;

public class ResultAssignedExamDto
{
    public Guid ExamId { get; set; }
    public string ExamName { get; set; }
    public int ExamMinute { get; set; }
    public string Description { get; set; }
    public int SuccessScore { get; set; }
}