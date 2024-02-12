using Application.Repositories.Question;
using Application.Repositories.QuestionCategory;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories.QuestionCategory;

public class QuestionCategoryRepository: BaseService<Domain.Entities.QuestionCategory>, IQuestionCategoryRepository
{
    public QuestionCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}