using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.ExamDtos;
using Application.DTOs.UserDto;
using Application.Repositories.Exam;
using Application.Repositories.ExamAnswer;
using Application.Repositories.Question;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ExamsController : ControllerBase
    {
        private readonly IExamRepository _examRepository;
        private readonly IExamAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;

        public ExamsController(IExamRepository examRepository, IExamAnswerRepository answerRepository,
            IQuestionRepository questionRepository)
        {
            _examRepository = examRepository;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
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
                ExamScenarioCategoryId = dto.ExamScenarioCategoryId
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
            value.ExamScenarioCategoryId = dto.ExamScenarioCategoryId;
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

        [HttpGet("exam/{examId}/all-user-scores")]
        public async Task<IActionResult> GetAllUserScoresForExam(Guid examId)
        {
            var userScores = new List<ResultUserDto>();

            var examQuestions = await _questionRepository
                .GetAll()
                .Include(q => q.QuestionCategory)
                .ThenInclude(qc => qc.Exam)
                .Where(q => q.QuestionCategory.ExamId == examId)
                .ToListAsync();

            var allUserAnswers = await _answerRepository
                .GetAll()
                .Include(ea => ea.ExamAssignment)
                .Where(ea => ea.ExamAssignment.ExamId == examId)
                .ToListAsync();

            var userIds = allUserAnswers.Select(ea => ea.UserId.ToString()).Distinct();

            foreach (var userId in userIds)
            {
                var userAnswers = allUserAnswers.Where(ea => ea.UserId.ToString() == userId).ToList();

                var correctAnswers = 0;
                foreach (var question in examQuestions)
                {
                    var userAnswer = userAnswers.FirstOrDefault(ea => ea.QuestionId == question.Id);

                    if (userAnswer != null && question.IsCorrect == userAnswer.UserAnswer)
                    {
                        correctAnswers++;
                    }
                }

                int totalScore = correctAnswers == examQuestions.Count ? 100 : correctAnswers * 50;

                var userScoreDto = new ResultUserDto
                {
                    UserId = userId,
                    ExamName = examQuestions.FirstOrDefault().QuestionCategory?.Exam.Name,
                    Score = totalScore,
                    AnsweredAt = DateTime.Now
                };

                userScores.Add(userScoreDto);
            }

            return Ok(userScores);
        }
    }
}