using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ChoiceDtos;
using Application.Repositories.Choice;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ChoicesController : ControllerBase
    {
        private readonly IChoiceRepository _choiceRepository;

        public ChoicesController(IChoiceRepository choiceRepository)
        {
            _choiceRepository = choiceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChoices()
        {
            var value = _choiceRepository.GetAll();
            return Ok(value);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdChoice(string id)
        {
            var value = await _choiceRepository.GetByIdAsync(id);
            return Ok(value);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateChoice(CreateChoiceDto dto)
        {
            var value = new Choice()
            {
                CreatedDate = DateTime.Now,
                Text = dto.Text,
                ChoiceType = dto.ChoiceType,
                QuestionId = dto.QuestionId
            };
            await _choiceRepository.AddAsync(value);
            await _choiceRepository.SaveAsync();
            return Ok(value.Id);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChoice(UpdateChoiceDto dto)
        {
            var value = await _choiceRepository.GetByIdAsync(dto.Id);
            value.Text = dto.Text;
            value.QuestionId = dto.QuestionId;
            value.ChoiceType = dto.ChoiceType;
            value.UpdatedDate = DateTime.Now;
            _choiceRepository.Update(value);
            await _choiceRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChoice(string id)
        {
            await _choiceRepository.RemoveAsync(id);
            await _choiceRepository.SaveAsync();
            return Ok();
        }
    }
}
