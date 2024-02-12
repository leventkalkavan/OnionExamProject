namespace Application.DTOs.ExamDtos;

public class UpdateExamDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int ExamMinute { get; set; }
    public string Description { get; set; }
    public int SuccessScore { get; set; }
    public Guid ScenarioCategoryId { get; set; }
}