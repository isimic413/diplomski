using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using ExamPreparation.DAL.Models;

namespace ExamPreparation.DAL
{
    public interface IExamPreparationContext : IDisposable
    {
        DbSet<AnswerChoice> AnswerChoices { get; set; }
        DbSet<AnswerChoicePicture> AnswerChoicePictures { get; set; }
        DbSet<AnswerStep> AnswerSteps { get; set; }
        DbSet<AnswerStepPicture> AnswerStepPictures { get; set; }
        DbSet<Exam> Exams { get; set; }
        DbSet<ExamProblem> ExamProblems { get; set; }
        DbSet<Problem> Problems { get; set; }
        DbSet<ProblemPicture> ProblemPictures { get; set; }
        DbSet<ProblemType> ProblemTypes { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<TestingArea> TestingAreas { get; set; }
        DbSet<TestingAreaProblem> TestingAreaProblems { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserAnswer> UserAnswers { get; set; }
        DbSet<UserRole> UserRoles { get; set; }


        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
