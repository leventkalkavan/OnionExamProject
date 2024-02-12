namespace Application.DTOs.ExamAssignment;

public class UpdateExamAssignmentDto
{
    public string Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ExamId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}