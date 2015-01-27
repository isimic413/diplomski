﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.DAL;
using ExamPreparation.DAL.Models;
using ExamPreparation.Repo.Common;

namespace ExamPreparation.Repo.Repository
{
    public class RepoAnswerStepPicture : Repo<AnswerStepPicture>, IRepoAnswerStepPicture
    {
        public RepoAnswerStepPicture(ExamPreparationContext dbContext)
            : base(dbContext)
        {
        }
    }
}
