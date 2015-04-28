﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface ITestingAreaService
    {
        Task<List<ITestingArea>> GetAsync(TestingAreaFilter filter);
        Task<ITestingArea> GetAsync(Guid id);
        Task<int> AddAsync(ITestingArea entity);
        Task<int> UpdateAsync(ITestingArea entity);
        Task<int> DeleteAsync(ITestingArea entity);
        Task<int> DeleteAsync(Guid id);
    }
}