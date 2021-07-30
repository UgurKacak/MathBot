using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public record QuestionCreated(Guid Id, Guid UserId, string Expression, string Result, DateTimeOffset CreatedOn);
}