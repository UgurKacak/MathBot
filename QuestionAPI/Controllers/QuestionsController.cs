using Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionAPI.Entity;
using QuestionAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionAPI.Controllers
{
    [ApiController]
    [Route("api/questions")]
    [Authorize]
        public class QuestionController : ControllerBase
        {
            private readonly IRepository<Question> repository;
            public readonly IPublishEndpoint publishEndpoint;

            public QuestionController(IRepository<Question> repository, IPublishEndpoint publishEndpoint)
            {
                this.repository = repository;
                this.publishEndpoint = publishEndpoint;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAsync()
            {
                var questions = (await repository.GetAllAsync()).Select(a => a.AsDto());
                return Ok(questions);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<QuestionDto>> GetByIdAsync(Guid id)
            {
                var question = await repository.GetAsync(id);
                if (question == null)
                {
                    NotFound();
                }

                return question.AsDto();
            }

            [HttpPost]
            public async Task<ActionResult<QuestionDto>> PostAsync(CreateQuestionDto createQuestionDto)
            {
                var question = new Question
                {
                    UserId = createQuestionDto.UserId,
                    UserName = createQuestionDto.UserName,
                    Expression = createQuestionDto.Expression,
                    Result = createQuestionDto.Result,
                    CreatedOn = DateTimeOffset.UtcNow
                };

                await repository.CreateAsync(question);

                await publishEndpoint.Publish(new QuestionCreated(question.Id, question.UserId, question.UserName, question.Expression, question.Result, question.CreatedOn));

                return CreatedAtAction(nameof(GetByIdAsync), new { id = question.Id }, question);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> PutAsync(Guid id, UpdateQuestionDto updateQuestionDto)
            {
                var existingQuestion = await repository.GetAsync(id);

                if (existingQuestion == null)
                {
                    return NotFound();
                }

                existingQuestion.UserId = updateQuestionDto.UserId;
                existingQuestion.UserName = updateQuestionDto.UserName;
                existingQuestion.Expression = updateQuestionDto.Expression;
                existingQuestion.Result = updateQuestionDto.Result;
                existingQuestion.ModifiedOn = DateTimeOffset.UtcNow;

                await repository.UpdateAsync(existingQuestion);

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteAsync(Guid id)
            {
                var question = await repository.GetAsync(id);

                if (question == null)
                {
                    return NotFound();
                }
                await repository.RemoveAsync(question.Id);

                return NoContent();
            }
        }
}
