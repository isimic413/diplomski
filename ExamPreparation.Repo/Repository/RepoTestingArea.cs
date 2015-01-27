using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.DAL;
using ExamPreparation.DAL.Models;
using ExamPreparation.Repo.Common;

namespace ExamPreparation.Repo.Repository
{
    public class RepoTestingArea : Repo<TestingArea>, IRepoTestingArea
    {
        public RepoTestingArea(ExamPreparationContext dbContext) : base(dbContext)
        {
        }
    }
}
