using Application.Repositories;
using Application.Repositories.ExamAssignment;
using Persistence.Context;

namespace Persistence.Repositories.ExamAssignment;

public class ExamAssignmentRepository: BaseService<Domain.Entities.ExamAssignment>, IExamAssignmentRepository
{
    public ExamAssignmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}