using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ExamPreparation.DAL.Models.Mapping;
using ExamPreparation.DAL.Models;

namespace ExamPreparation.DAL
{
    public partial class ExamPreparationContext : DbContext, IExamPreparationContext
    {
        static ExamPreparationContext()
        {
            Database.SetInitializer<ExamPreparationContext>(null);
        }

        public ExamPreparationContext()
            : base("Name=ExamPreparationContext")
        {
        }

        public DbSet<AnswerChoice> AnswerChoices { get; set; }
        public DbSet<AnswerChoicePicture> AnswerChoicePictures { get; set; }
        public DbSet<AnswerStep> AnswerSteps { get; set; }
        public DbSet<AnswerStepPicture> AnswerStepPictures { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamProblem> ExamProblems { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<ProblemPicture> ProblemPictures { get; set; }
        public DbSet<ProblemType> ProblemTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TestingArea> TestingAreas { get; set; }
        public DbSet<TestingAreaProblem> TestingAreaProblems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnswerChoiceMap());
            modelBuilder.Configurations.Add(new AnswerChoicePictureMap());
            modelBuilder.Configurations.Add(new AnswerStepMap());
            modelBuilder.Configurations.Add(new AnswerStepPictureMap());
            modelBuilder.Configurations.Add(new ExamMap());
            modelBuilder.Configurations.Add(new ExamProblemMap());
            modelBuilder.Configurations.Add(new ProblemMap());
            modelBuilder.Configurations.Add(new ProblemPictureMap());
            modelBuilder.Configurations.Add(new ProblemTypeMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new TestingAreaMap());
            modelBuilder.Configurations.Add(new TestingAreaProblemMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserAnswerMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
        }
    }
}
