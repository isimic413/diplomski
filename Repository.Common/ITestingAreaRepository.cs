﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface ITestingAreaRepository
    {
        Task<List<ITestingArea>> GetAsync(TestingAreaFilter filter = null);
        Task<ITestingArea> GetAsync(Guid id);

        Task<int> InsertAsync(ITestingArea entity);

        Task<int> UpdateAsync(ITestingArea entity);

        Task<int> DeleteAsync(ITestingArea entity);
        Task<int> DeleteAsync(Guid id);
    }
}
