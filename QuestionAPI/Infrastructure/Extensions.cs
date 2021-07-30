using QuestionAPI.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuestionAPI.Infrastructure
{
    public static class Extensions
    {
        public static QuestionDto AsDto(this Question question)
        {
            return new QuestionDto(question.Id, question.UserId, question.Expression, question.Result, question.CreatedOn, question.ModifiedOn);
        }
    }
}
