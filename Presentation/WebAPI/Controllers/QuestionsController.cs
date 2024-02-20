using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.QuestionDtos;
using Application.Repositories.Question;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var value = _questionRepository.GetAll();
            return Ok(value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdQuestion(string id)
        {
            var value = await _questionRepository.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateQuestionDto dto)
        {
            var value = new Question()
            {
                QuestionCategoryId = dto.QuestionCategoryId,
                CreatedDate = DateTime.Now,
                Description = dto.Description,
                IsCorrect = dto.IsCorrect
            };
            await _questionRepository.AddAsync(value);
            await _questionRepository.SaveAsync();
            return Ok(value.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(UpdateQuestionDto dto)
        {
            var value = await _questionRepository.GetByIdAsync(dto.Id);
            value.IsCorrect = dto.IsCorrect;
            value.Description = dto.Description;
            value.QuestionCategoryId = dto.QuestionCategoryId;
            value.UpdatedDate = DateTime.Now;
            _questionRepository.Update(value);
            await _questionRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            await _questionRepository.RemoveAsync(id);
            await _questionRepository.SaveAsync();
            return Ok();
        }
    }
}