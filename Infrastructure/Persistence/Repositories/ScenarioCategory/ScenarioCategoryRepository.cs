using Application.Repositories.ScenarioCategory;
using Persistence.Context;

namespace Persistence.Repositories.ScenarioCategory;

public class ScenarioCategoryRepository: BaseService<Domain.Entities.ScenarioCategory>, IScenarioCategoryRepository
{
    public ScenarioCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}