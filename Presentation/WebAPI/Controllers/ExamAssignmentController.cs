using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ExamAssignment;
using Application.Repositories.ExamAssignment;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = "Admin")]
    public class ExamAssignmentController : ControllerBase
    {
        private readonly IExamAssignmentRepository _assignmentRepository;

        public ExamAssignmentController(IExamAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
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
            var value = new ExamAssignment()
            {
                CreatedDate = DateTime.Now,
                UserId = dto.UserId,
                ExamId = dto.ExamId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };
            await _assignmentRepository.AddAsync(value);
            await _assignmentRepository.SaveAsync();
            return Ok(value.Id);
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
