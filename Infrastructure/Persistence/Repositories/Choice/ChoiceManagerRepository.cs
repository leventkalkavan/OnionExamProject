using Application.Repositories.Choice;
using Persistence.Context;

namespace Persistence.Repositories.Choice;

public class ChoiceManagerRepository: BaseService<Domain.Entities.Choice>, IChoiceRepository
{
    public ChoiceManagerRepository(ApplicationDbContext context) : base(context)
    {
    }
}