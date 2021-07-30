using Infrastructure;
using MassTransit;
using QuestionConsumer.Entity;
using System;
using System.Data;
using System.Threading.Tasks;

namespace QuestionConsumer.Consumer
{
    public class QuestionCreatedConsumer : IConsumer<QuestionCreated>
    {
        private readonly IRepository<Question> repository;
        public QuestionCreatedConsumer(IRepository<Question> repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<QuestionCreated> context)
        {

            var message = context.Message;


            double eResult = Convert.ToDouble(new DataTable().Compute(message.Expression, null));

            var question = new Question
            {
                Id = message.Id,
                UserId = message.UserId,
                Expression = message.Expression,
                Result = eResult.ToString(),
                CreatedOn = message.CreatedOn,
                ModifiedOn = DateTimeOffset.UtcNow
            };

            Console.WriteLine("Question Created -> " + message.Expression + " = " + eResult.ToString());
            Console.WriteLine("Synthetic 10sec waiting before update.");
            Task.Delay(10000).Wait();

            await repository.UpdateAsync(question);
        }
    }
}
