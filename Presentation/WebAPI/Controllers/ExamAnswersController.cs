using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ExamAnswerDtos;
using Application.Repositories.ExamAnswer;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = "Admin")]
    public class ExamAnswersController : ControllerBase
    {
        private readonly IExamAnswerRepository _answerRepository;

        public ExamAnswersController(IExamAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExamAnswers()
        {
            var value = _answerRepository.GetAll();
            return Ok(value);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdExamAnswer(string id)
        {
            var value = await _answerRepository.GetByIdAsync(id);
            return Ok(value);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateExamAnswer(CreateExamAnswerDto dto)
        {
            var value = new ExamAnswer()
            {
                CreatedDate = DateTime.Now,
                UserId = dto.UserId,
                ExamAssignmentId = dto.ExamAssignmentId,
                Score = dto.Score,
                SelectedChoiceId = dto.SelectedChoiceId,
                AnsweredAt = DateTime.Now,
                ExamId = dto.ExamId,
                QuestionId = dto.QuestionId
            };
            await _answerRepository.AddAsync(value);
            await _answerRepository.SaveAsync();
            return Ok(value.Id);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExamAnswer(UpdateExamAnswerDto dto)
        {
            var value = await _answerRepository.GetByIdAsync(dto.Id);
            value.UserId = dto.UserId;
            value.QuestionId = dto.QuestionId;
            value.ExamAssignmentId = dto.ExamAssignmentId;
            value.Score = dto.Score;
            value.SelectedChoiceId = dto.SelectedChoiceId;
            value.AnsweredAt = dto.AnsweredAt;
            value.ExamId = dto.ExamId;
            value.UpdatedDate = DateTime.Now;
            _answerRepository.Update(value);
            await _answerRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamAnswer(string id)
        {
            await _answerRepository.RemoveAsync(id);
            await _answerRepository.SaveAsync();
            return Ok();
        }
    }
}
