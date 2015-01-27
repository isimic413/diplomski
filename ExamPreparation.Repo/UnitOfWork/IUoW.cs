using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExamPreparation.DAL;
using ExamPreparation.DAL.Models;
using ExamPreparation.Repo.Common;

namespace ExamPreparation.Repo.UnitOfWork
{
    public interface IUoW
    {
        ExamPreparationContext DbContext { get; set; }

        void Commit();
        void Dispose();

        IRepository<AnswerChoice> AnswerChoices { get; }
        IRepository<AnswerChoicePicture> AnswerChoicePictures { get; }
        IRepository<AnswerStep> AnswerSteps { get; }
        IRepository<AnswerStepPicture> AnswerStepPictures { get; }
        IRepository<Exam> Exams { get; }
        IRepository<ExamProblem> ExamProblems { get; }
        IRepository<Problem> Problems { get; }
        IRepository<ProblemPicture> ProblemPictures { get; }
        IRepository<ProblemType> ProblemTypes { get; }
        IRepository<Role> Roles { get; }
        IRepository<TestingArea> TestingAreas { get; }
        IRepository<TestingAreaProblem> TestingAreaProblems { get; }
        IRepository<User> Users { get; }
        IRepository<UserAnswer> UserAnswers { get; }
        IRepository<UserRole> UserRoles { get; } 
    }
}
