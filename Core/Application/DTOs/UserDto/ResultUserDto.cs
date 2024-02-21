namespace Application.DTOs.UserDto;

public class ResultUserDto
{
    public string UserId { get; set; }
    public string ExamName { get; set; }
    public int Score { get; set; }
    public DateTime AnsweredAt { get; set; }
}