using Application.Repositories.ScenarioCategory;
using Persistence.Context;

namespace Persistence.Repositories.ExamScenarioCategory;

public class ExamScenarioCategoryRepository: BaseService<Domain.Entities.ExamScenarioCategory>, IExamScenarioCategoryRepository
{
    public ExamScenarioCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}