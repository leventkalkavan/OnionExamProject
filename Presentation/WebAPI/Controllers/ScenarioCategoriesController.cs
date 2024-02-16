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
    public class ScenarioCategoriesController : ControllerBase
    {
        private readonly IScenarioCategoryRepository _scenarioCategoryRepository;

        public ScenarioCategoriesController(IScenarioCategoryRepository scenarioCategoryRepository)
        {
            _scenarioCategoryRepository = scenarioCategoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllScenarioCategories()
        {
            var value = _scenarioCategoryRepository.GetAll();
            return Ok(value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdScenarioCategory(string id)
        {
            var value = await _scenarioCategoryRepository.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateScenarioCategory(CreateScenarioCategoryDto dto)
        {
            var value = new ScenarioCategory()
            {
                Name = dto.Name,
                CreatedDate = DateTime.Now
            };
            await _scenarioCategoryRepository.AddAsync(value);
            await _scenarioCategoryRepository.SaveAsync();
            return Ok(value.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScenarioCategory(UpdateScenarioCategoryDto dto)
        {
            var value = await _scenarioCategoryRepository.GetByIdAsync(dto.Id);
            value.Name = dto.Name;
            value.UpdatedDate = DateTime.Now;
            _scenarioCategoryRepository.Update(value);
            await _scenarioCategoryRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScenarioCategory(string id)
        {
            await _scenarioCategoryRepository.RemoveAsync(id);
            await _scenarioCategoryRepository.SaveAsync();
            return Ok();
        }
    }
}