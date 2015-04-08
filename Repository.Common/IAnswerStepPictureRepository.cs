﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IAnswerStepPictureRepository
    {
        Task<List<IAnswerStepPicture>> GetAsync(string sortOrder = "stepId", int pageNumber = 0, int pageSize = 50);
        Task<IAnswerStepPicture> GetAsync(Guid id);
        Task<int> AddAsync(IAnswerStepPicture entity);
        Task<int> UpdateAsync(IAnswerStepPicture entity);
        Task<int> DeleteAsync(IAnswerStepPicture entity);
        Task<int> DeleteAsync(Guid id);
    }
}