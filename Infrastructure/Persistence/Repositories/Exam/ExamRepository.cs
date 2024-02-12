using Application.Repositories.Exam;
using Persistence.Context;

namespace Persistence.Repositories.Exam;

public class ExamRepository: BaseService<Domain.Entities.Exam>, IExamRepository
{
    public ExamRepository(ApplicationDbContext context) : base(context)
    {
    }
}