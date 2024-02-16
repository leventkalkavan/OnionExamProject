using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ExamAssignment;
using Application.Repositories.ExamAssignment;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ExamAssignmentController : ControllerBase
    {
        private readonly IExamAssignmentRepository _assignmentRepository;
        private readonly UserManager<AppUser> _userManager;
        public ExamAssignmentController(IExamAssignmentRepository assignmentRepository, UserManager<AppUser> userManager)
        {
            _assignmentRepository = assignmentRepository;
            _userManager = userManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllExamAssignments()
        {
            var value = _assignmentRepository.GetAll();
            return Ok(value);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdExamAssignment(string id)
        {
            var value = await _assignmentRepository.GetByIdAsync(id);
            return Ok(value);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateExamAssignment(CreateExamAssignmentDto dto)
        {
            var appUser = await _userManager.FindByIdAsync(dto.UserId.ToString());

            if (appUser == null)
            {
                return NotFound("User not found");
            }
            var examAssignment = new ExamAssignment()
            {
                CreatedDate = DateTime.Now,
                UserId = new Guid(appUser.Id),
                ExamId = dto.ExamId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
            };

            await _assignmentRepository.AddAsync(examAssignment);
            await _assignmentRepository.SaveAsync();

            return Ok(examAssignment.Id);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExamAssignment(UpdateExamAssignmentDto dto)
        {
            var value = await _assignmentRepository.GetByIdAsync(dto.Id);
            value.UserId = dto.UserId;
            value.ExamId = dto.ExamId;
            value.StartTime = dto.StartTime;
            value.EndTime = dto.EndTime;
            value.UpdatedDate = DateTime.Now;
            _assignmentRepository.Update(value);
            await _assignmentRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamAssignment(string id)
        {
            await _assignmentRepository.RemoveAsync(id);
            await _assignmentRepository.SaveAsync();
            return Ok();
        }
    }
}
