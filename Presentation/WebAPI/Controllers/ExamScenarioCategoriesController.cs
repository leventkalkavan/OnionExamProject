using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ScenarioCategoryDtos;
using Application.Repositories.ScenarioCategory;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ExamScenarioCategoriesController : ControllerBase
    {
        private readonly IExamScenarioCategoryRepository _examScenarioCategoryRepository;

        public ExamScenarioCategoriesController(IExamScenarioCategoryRepository examScenarioCategoryRepository)
        {
            _examScenarioCategoryRepository = examScenarioCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExamScenarioCategories()
        {
            var value = _examScenarioCategoryRepository.GetAll();
            return Ok(value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdlExamScenarioCategory(string id)
        {
            var value = await _examScenarioCategoryRepository.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExamScenarioCategory(CreateExamScenarioCategoryDto dto)
        {
            var value = new ExamScenarioCategory()
            {
                Name = dto.Name,
                CreatedDate = DateTime.Now
            };
            await _examScenarioCategoryRepository.AddAsync(value);
            await _examScenarioCategoryRepository.SaveAsync();
            return Ok(value.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExamScenarioCategory(UpdateExamScenarioCategoryDto dto)
        {
            var value = await _examScenarioCategoryRepository.GetByIdAsync(dto.Id);
            value.Name = dto.Name;
            value.UpdatedDate = DateTime.Now;
            _examScenarioCategoryRepository.Update(value);
            await _examScenarioCategoryRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExamScenarioCategory(string id)
        {
            await _examScenarioCategoryRepository.RemoveAsync(id);
            await _examScenarioCategoryRepository.SaveAsync();
            return Ok();
        }
    }
}