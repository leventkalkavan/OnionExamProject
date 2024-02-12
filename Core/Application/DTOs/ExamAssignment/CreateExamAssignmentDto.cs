namespace Application.DTOs.ExamAssignment;

public class CreateExamAssignmentDto
{
    public Guid UserId { get; set; }
    public Guid ExamId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}