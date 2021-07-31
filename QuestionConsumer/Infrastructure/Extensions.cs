using QuestionConsumer.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace QuestionConsumer.Infrastructure
{
    public static class Extensions
    {
        public static QuestionDto AsDto(this Question question)
        {
            return new QuestionDto(question.Id, question.UserId, question.UserName, question.Expression, question.Result, question.CreatedOn, question.ModifiedOn);
        }
    }
}
