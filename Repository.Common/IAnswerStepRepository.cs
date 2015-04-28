﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerStepRepository
    {
        Task<List<IAnswerStep>> GetAsync(AnswerStepFilter filter);
        Task<IAnswerStep> GetAsync(Guid id);
        Task<int> AddAsync(IAnswerStep entity, IAnswerStepPicture picture = null);
        Task<int> UpdateAsync(IAnswerStep entity, IAnswerStepPicture picture = null);
        Task<int> DeleteAsync(IAnswerStep entity);
        Task<int> DeleteAsync(Guid id);

        Task<List<IAnswerStep>> GetStepsAsync(Guid questionId);
    }
}
