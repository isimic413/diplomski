using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.DAL.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ExamPreparation.DAL
{
    class ExamPreparationContext : DbContext
    {
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
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Problem
            modelBuilder.Entity<AnswerStepPicture>()
                .HasRequired(c => c.AnswerStep);
            modelBuilder.Entity<AnswerStep>()
                .HasRequired(c => c.Problem)
                .WithMany(q => q.Steps);

            modelBuilder.Entity<AnswerChoicePicture>()
                .HasRequired(c => c.AnswerChoice);
            modelBuilder.Entity<AnswerChoice>()
                .HasRequired(c => c.Problem)
                .WithMany(q => q.Choices);

            modelBuilder.Entity<ProblemPicture>()
                .HasRequired(c => c.Problem);

            modelBuilder.Entity<TestingAreaProblem>()
                .HasRequired(c => c.Problem)
                .WithMany(q => q.TestingAreas);

            modelBuilder.Entity<ExamProblem>()
                .HasRequired(c => c.Problem)
                .WithMany(q => q.Exams);

            modelBuilder.Entity<UserAnswer>()
                .HasRequired(c => c.Problem)
                .WithMany(q => q.UserAnswers);

            // ProblemType
            modelBuilder.Entity<Problem>()
                .HasRequired(c => c.ProblemType)
                .WithMany(q => q.Problems);
            
            // TestingArea
            modelBuilder.Entity<TestingAreaProblem>()
                .HasRequired(c => c.TestingArea)
                .WithMany(q => q.Problems);

            modelBuilder.Entity<Exam>()
                .HasRequired(c => c.TestingArea)
                .WithMany(q => q.Exams);

            // Exam
            modelBuilder.Entity<ExamProblem>()
                .HasRequired(c => c.Exam)
                .WithMany(q => q.Problems);

            // Role
            modelBuilder.Entity<UserRole>()
                .HasRequired(c => c.Role)
                .WithMany(q => q.Users);

            // User
            modelBuilder.Entity<UserRole>()
                .HasRequired(c => c.User)
                .WithMany(q => q.Roles);

            modelBuilder.Entity<UserAnswer>()
                .HasRequired(c => c.User)
                .WithMany(q => q.Answers);

            base.OnModelCreating(modelBuilder);
        }
    }
}
