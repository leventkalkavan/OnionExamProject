using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Repositories.ExamAssignment;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IExamAssignmentRepository _assignmentRepository;

        public UsersController(IExamAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAssignedExams(string userId)
        {
            var assignedExams = await _assignmentRepository
                .GetAll()
                .Where(a => a.UserId.ToString() == userId)
                .Include(a => a.Exam)
                .Select(a => a.Exam)
                .ToListAsync();

            if (assignedExams == null || assignedExams.Count == 0)
            {
                return NotFound($"No exams assigned to the user with ID: {userId}");
            }

            var assignedExamDTOs = assignedExams.Select(exam => new
            {
                ExamId = exam.Id,
                ExamName = exam.Name,
            });

            return Ok(assignedExamDTOs);
        }

        [HttpGet("assigned-exams")]
        [Authorize]
        public async Task<IActionResult> GetAssignedExamsForAuth()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID not found in claims.");
            }

            var assignedExams = await _assignmentRepository
                .GetAll()
                .Where(a => a.UserId.ToString() == userId)
                .Include(a => a.Exam)
                .Select(a => a.Exam)
                .ToListAsync();
            var assignedExamDTOs = assignedExams.Select(exam => new
            {
                ExamId = exam.Id,
                ExamName = exam.Name,
            });

            return Ok(assignedExamDTOs);
        }
    }
}