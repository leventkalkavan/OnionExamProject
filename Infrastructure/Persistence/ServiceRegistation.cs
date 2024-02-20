using Application.Abstractions.Services;
using Application.Repositories.Exam;
using Application.Repositories.ExamAnswer;
using Application.Repositories.ExamAssignment;
using Application.Repositories.Question;
using Application.Repositories.QuestionCategory;
using Application.Repositories.ScenarioCategory;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories.Exam;
using Persistence.Repositories.ExamAnswer;
using Persistence.Repositories.ExamAssignment;
using Persistence.Repositories.ExamScenarioCategory;
using Persistence.Repositories.Question;
using Persistence.Repositories.QuestionCategory;
using Persistence.Services;

namespace Persistence;

public static class ServiceRegistation
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString));
        services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddSignInManager().AddRoles<IdentityRole>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IExamScenarioCategoryRepository, ExamScenarioCategoryRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<IQuestionCategoryRepository, QuestionCategoryRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IExamAssignmentRepository, ExamAssignmentRepository>();
        services.AddScoped<IExamAnswerRepository, ExamAnswerRepository>();
    }
}