using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExamPreparation.DAL;
using ExamPreparation.DAL.Common;
using ExamPreparation.DAL.Models;
using ExamPreparation.Repo.Common;
using ExamPreparation.Repo.Repository;

namespace ExamPreparation.Repo.UnitOfWork
{
    public class UoW : IUoW, IDisposable
    {
        public ExamPreparationContext DbContext { get; set; }

        public UoW()
        {
            CreateDbContext();
        }


        private IRepository<AnswerChoice> _answerChoices { get; set; }
        private IRepository<AnswerChoicePicture> _answerChoicePictures { get; set; }
        private IRepository<AnswerStep> _answerSteps { get; set; }
        private IRepository<AnswerStepPicture> _answerStepPictures { get; set; }
        private IRepository<Exam> _exams { get; set; }
        private IRepository<ExamProblem> _examProblems { get; set; }
        private IRepository<Problem> _problems { get; set; }
        private IRepository<ProblemPicture> _problemPictures { get; set; }
        private IRepository<ProblemType> _problemTypes { get; set; }
        private IRepository<Role> _roles { get; set; }
        private IRepository<TestingArea> _testingAreas { get; set; }
        private IRepository<TestingAreaProblem> _testingAreaProblems { get; set; }
        private IRepository<User> _users { get; set; }
        private IRepository<UserAnswer> _userAnswers { get; set; }
        private IRepository<UserRole> _userRoles { get; set; }


        public IRepository<AnswerChoice> AnswerChoices
        {
            get
            {
                if (_answerChoices == null)
                {
                    _answerChoices = new RepoAnswerChoice(DbContext);
                }
                return _answerChoices;
            }
        }

        public IRepository<AnswerChoicePicture> AnswerChoicePictures
        {
            get
            {
                if (_answerChoicePictures == null)
                {
                    _answerChoicePictures = new RepoAnswerChoicePicture(DbContext);
                }
                return _answerChoicePictures;
            }
        }

        public IRepository<AnswerStep> AnswerSteps
        {
            get
            {
                if (_answerSteps == null)
                {
                    _answerSteps = new RepoAnswerStep(DbContext);
                }
                return _answerSteps;
            }
        }

        public IRepository<AnswerStepPicture> AnswerStepPictures
        {
            get
            {
                if (_answerStepPictures == null)
                {
                    _answerStepPictures = new RepoAnswerStepPicture(DbContext);
                }
                return _answerStepPictures;
            }
        }

        public IRepository<Exam> Exams
        {
            get
            {
                if (_exams == null)
                {
                    _exams = new RepoExam(DbContext);
                }
                return _exams;
            }
        }

        public IRepository<ExamProblem> ExamProblems
        {
            get
            {
                if (_examProblems == null)
                {
                    _examProblems = new RepoExamProblem(DbContext);
                }
                return _examProblems;
            }
        }

        public IRepository<Problem> Problems
        {
            get
            {
                if (_problems == null)
                {
                    _problems = new RepoProblem(DbContext);
                }
                return _problems;
            }
        }

        public IRepository<ProblemPicture> ProblemPictures
        {
            get
            {
                if (_problemPictures == null)
                {
                    _problemPictures = new RepoProblemPicture(DbContext);
                }
                return _problemPictures;
            }
        }

        public IRepository<ProblemType> ProblemTypes
        {
            get
            {
                if (_problemTypes == null)
                {
                    _problemTypes = new RepoProblemType(DbContext);
                }
                return _problemTypes;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RepoRole(DbContext);
                }
                return _roles;
            }
        }
        public IRepository<TestingArea> TestingAreas
        {
            get
            {
                if (_testingAreas == null)
                {
                    _testingAreas = new RepoTestingArea(DbContext);
                }
                return _testingAreas;
            }
        }

        public IRepository<TestingAreaProblem> TestingAreaProblems
        {
            get
            {
                if (_testingAreaProblems == null)
                {
                    _testingAreaProblems = new RepoTestingAreaProblem(DbContext);
                }
                return _testingAreaProblems;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new RepoUser(DbContext);
                }
                return _users;
            }
        }

        public IRepository<UserAnswer> UserAnswers
        {
            get
            {
                if (_userAnswers == null)
                {
                    _userAnswers = new RepoUserAnswer(DbContext);
                }
                return _userAnswers;
            }
        }

        public IRepository<UserRole> UserRoles
        {
            get
            {
                if (_userRoles == null)
                {
                    _userRoles = new RepoUserRole(DbContext);
                }
                return _userRoles;
            }
        }



        protected void CreateDbContext()
        {
            DbContext = new ExamPreparationContext();
            DbContext.Configuration.LazyLoadingEnabled = false;
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }
    }
}
