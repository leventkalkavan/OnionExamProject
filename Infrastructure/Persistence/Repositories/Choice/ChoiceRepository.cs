using Application.Repositories.Choice;
using Persistence.Context;

namespace Persistence.Repositories.Choice;

public class ChoiceRepository: BaseService<Domain.Entities.Choice>, IChoiceRepository
{
    public ChoiceRepository(ApplicationDbContext context) : base(context)
    {
    }
}