using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.DTOs.ChoiceDtos;
using Application.DTOs.ExamAssignment;
using Application.DTOs.QuestionDtos;
using Application.Repositories.Choice;
using Application.Repositories.ExamAssignment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Authorize]
    public class UserExamsController : Controller
    {
        private readonly IExamAssignmentRepository _assignmentRepository;
        private readonly IChoiceRepository _choiceRepository;

        public UserExamsController(IExamAssignmentRepository assignmentRepository, IChoiceRepository choiceRepository)
        {
            _assignmentRepository = assignmentRepository;
            _choiceRepository = choiceRepository;
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

        [HttpGet("user-assigned-questions")]
        public async Task<IActionResult> GetUserAssignedQuestions()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found in claims.");
            }

            var assignedExams = await _assignmentRepository
                .GetAll()
                .Include(a => a.Exam)
                .ThenInclude(e => e.QuestionCategories)
                .ThenInclude(qc => qc.Questions)
                .ThenInclude(q => q.Choices)
                .Where(a => a.UserId.ToString() == userId)
                .ToListAsync();

            if (assignedExams == null || !assignedExams.Any())
            {
                return NotFound("No assigned exams found for the user.");
            }

            var questionDtos = assignedExams
                .SelectMany(a => a.Exam.QuestionCategories)
                .SelectMany(qc => qc.Questions)
                .Select(q => new ResultQuestionDto()
                {
                    QuestionId = q.Id,
                    Description = q.Description,
                    Choices = q.Choices.Select(c => new ResultChoiceDto()
                    {
                        ChoiceId = c.Id.ToString(),
                        ChoiceText = c.Text,
                        ChoiceType = c.ChoiceType
                    }).ToList()
                })
                .ToList();

            return Ok(questionDtos);
        }

        [HttpGet("choice-by-id/{choiceId}")]
        public async Task<IActionResult> GetChoiceById(string choiceId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found in claims.");
            }

            var assignedChoice = await _assignmentRepository
                .GetAll()
                .Include(a => a.Exam)
                .ThenInclude(e => e.QuestionCategories)
                .ThenInclude(qc => qc.Questions)
                .ThenInclude(q => q.Choices)
                .Where(a => a.UserId.ToString() == userId)
                .SelectMany(a => a.Exam.QuestionCategories.SelectMany(qc => qc.Questions.SelectMany(q => q.Choices)))
                .FirstOrDefaultAsync(c => c.Id.ToString() == choiceId);

            if (assignedChoice == null)
            {
                return NotFound("Choice not found.");
            }

            var choiceDto = new ResultChoiceDto
            {
                ChoiceId = assignedChoice.Id.ToString(),
                ChoiceText = assignedChoice.Text,
                ChoiceType = assignedChoice.ChoiceType
            };

            return Ok(choiceDto);
        }
    }
}