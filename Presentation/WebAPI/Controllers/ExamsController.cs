using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ExamDtos;
using Application.Repositories.Exam;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = "Admin")]
    public class ExamsController : ControllerBase
    {
        private readonly IExamRepository _examRepository;

        public ExamsController(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExams()
        {
            var value = _examRepository.GetAll();
            return Ok(value);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdExam(string id)
        {
            var value = await _examRepository.GetByIdAsync(id);
            return Ok(value);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateExam(CreateExamDto dto)
        {
            var value = new Exam()
            {
                Name = dto.Name,
                CreatedDate = DateTime.Now,
                ExamMinute = dto.ExamMinute,
                Description = dto.Description,
                SuccessScore = dto.SuccessScore,
                ScenarioCategoryId = dto.ScenarioCategoryId
            };
            await _examRepository.AddAsync(value);
            await _examRepository.SaveAsync();
            return Ok(value.Id);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(UpdateExamDto dto)
        {
            var value = await _examRepository.GetByIdAsync(dto.Id);
            value.Name = dto.Name;
            value.ExamMinute = dto.ExamMinute;
            value.Description = dto.Description;
            value.SuccessScore = dto.SuccessScore;
            value.ScenarioCategoryId = dto.ScenarioCategoryId;
            value.UpdatedDate = DateTime.Now;
            _examRepository.Update(value);
            await _examRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(string id)
        {
            await _examRepository.RemoveAsync(id);
            await _examRepository.SaveAsync();
            return Ok();
        }

    }
}
