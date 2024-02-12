using Application.Repositories.ExamAnswer;
using Persistence.Context;

namespace Persistence.Repositories.ExamAnswer;

public class ExamAnswerRepository: BaseService<Domain.Entities.ExamAnswer>, IExamAnswerRepository
{
    public ExamAnswerRepository(ApplicationDbContext context) : base(context)
    {
    }
}