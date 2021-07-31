using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionConsumer.Infrastructure
{
    public record QuestionDto(Guid Id, Guid UserId, string UserName, string Expression, string Result, DateTimeOffset CreatedOn, DateTimeOffset ModifiedOn);

    public record CreateQuestionDto([Required] Guid UserId, [Required] string UserName, [Required] string Expression, string Result = "On progress.");

    public record UpdateQuestionDto([Required] Guid UserId, [Required] string UserName, [Required] string Expression, [Required] string Result);

}