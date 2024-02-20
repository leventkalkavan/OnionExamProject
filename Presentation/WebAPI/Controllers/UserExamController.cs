using System.Security.Claims;
using Application.DTOs.ExamAnswerDtos;
using Application.DTOs.ExamAssignment;
using Application.Repositories.ExamAnswer;
using Application.Repositories.ExamAssignment;
using Application.Repositories.Question;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Authorize]
    public class UserExamsController : Controller
    {
        private readonly IExamAssignmentRepository _assignmentRepository;
        private readonly IExamAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;

        public UserExamsController(IExamAssignmentRepository assignmentRepository,
            IExamAnswerRepository answerRepository, IQuestionRepository questionRepository)
        {
            _assignmentRepository = assignmentRepository;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
        }

        [HttpGet("assigned-exams")]
        public async Task<IActionResult> GetAssignedExamsForAuth()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var assignedExams = await _assignmentRepository
                .GetAll()
                .Where(a => a.UserId.ToString() == userId)
                .Select(a => new ResultAssignedExamDto()
                {
                    ExamId = a.Exam.Id,
                    ExamName = a.Exam.Name,
                    ExamMinute = a.Exam.ExamMinute,
                    Description = a.Exam.Description,
                    SuccessScore = a.Exam.SuccessScore
                })
                .ToListAsync();
            return Ok(assignedExams);
        }

        [HttpPost("assigned-exams/{examId}/answer")]
        public async Task<IActionResult> PostAnswerForAssignedExam(Guid examId, [FromBody] SubmitAnswerDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var assignment = await _assignmentRepository
                .GetAll()
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId && a.ExamId == examId);

            if (assignment == null)
            {
                return BadRequest("An exam assigned to the user and not completed was not found.");
            }

            var question = await _questionRepository.GetByIdAsync(dto.QuestionId.ToString());

            if (question == null)
            {
                return BadRequest("The specified question was not found.");
            }

            var userAnswerIsCorrect = question.IsCorrect == (dto.UserAnswer);

            var examAnswer = new ExamAnswer
            {
                UserId = Guid.Parse(userId),
                ExamAssignmentId = assignment.Id,
                QuestionId = dto.QuestionId,
                UserAnswer = dto.UserAnswer,
                Score = userAnswerIsCorrect ? 1 : 0,
                AnsweredAt = DateTime.UtcNow
            };

            await _answerRepository.AddAsync(examAnswer);
            await _answerRepository.SaveAsync();

            return Ok("The exam answer was successfully recorded.");
        }
        [HttpGet("assigned-exams/{examId}/user-score")]
        public async Task<IActionResult> GetUserScoreForAssignedExam(Guid examId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var userAnswers = await _answerRepository
                    .GetAll()
                    .Where(ea => ea.UserId.ToString() == userId && ea.ExamAssignment.ExamId == examId)
                    .ToListAsync();
                var examQuestions = await _questionRepository
                    .GetAll()
                    .Where(q => q.QuestionCategory.ExamId == examId)
                    .ToListAsync();

                var correctAnswers = 0;
                foreach (var question in examQuestions)
                {
                    var userAnswer = userAnswers.FirstOrDefault(ea => ea.QuestionId == question.Id);

                    if (userAnswer != null && question.IsCorrect == userAnswer.UserAnswer)
                    {
                        correctAnswers++;
                    }
                }
                int totalScore = correctAnswers == examQuestions.Count ? 100 : correctAnswers * 50;

                return Ok(new { TotalScore = totalScore });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}