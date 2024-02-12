using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.QuestionCategoryDtos;
using Application.Repositories.QuestionCategory;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = "Admin")]
    public class QuetionCategoriesController : ControllerBase
    {
        private readonly IQuestionCategoryRepository _questionCategoryRepository;

        public QuetionCategoriesController(IQuestionCategoryRepository categoryRepository)
        {
            _questionCategoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestionCategories()
        {
            var value = _questionCategoryRepository.GetAll();
            return Ok(value);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdQuestionCategory(string id)
        {
            var value = await _questionCategoryRepository.GetByIdAsync(id);
            return Ok(value);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateQuestionCategory(CreateQuestionCategoryDto dto)
        {
            var value = new QuestionCategory()
            {
                Name = dto.Name,
                CreatedDate = DateTime.Now,
                ExamId = dto.ExamId
            };
            await _questionCategoryRepository.AddAsync(value);
            await _questionCategoryRepository.SaveAsync();
            return Ok(value.Id);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestionCategory(UpdateQuestionCategoryDto dto)
        {
            var value = await _questionCategoryRepository.GetByIdAsync(dto.Id);
            value.Name = dto.Name;
            value.ExamId = dto.ExamId;
            value.UpdatedDate = DateTime.Now;
            _questionCategoryRepository.Update(value);
            await _questionCategoryRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionCategory(string id)
        {
            await _questionCategoryRepository.RemoveAsync(id);
            await _questionCategoryRepository.SaveAsync();
            return Ok();
        }

    }
}
