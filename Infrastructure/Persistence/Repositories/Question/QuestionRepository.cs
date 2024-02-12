using Application.Repositories.Question;
using Persistence.Context;

namespace Persistence.Repositories.Question;

public class QuestionRepository: BaseService<Domain.Entities.Question>, IQuestionRepository
{
    public QuestionRepository(ApplicationDbContext context) : base(context)
    {
    }
}