using Application.Repositories.Exam;
using Persistence.Context;

namespace Persistence.Repositories.Exam;

public class ExamManagerRepository: BaseService<Domain.Entities.Exam>, IExamRepository
{
    public ExamManagerRepository(ApplicationDbContext context) : base(context)
    {
    }
}