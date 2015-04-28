﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPreparation.Repository.Common
{
    public interface IRepository
    {
        IUnitOfWork CreateUnitOfWork();

        // Task<IQueryable<T>>?
        IQueryable<T> WhereAsync<T>() where T : class;
        Task<T> SingleAsync<T>(Guid id) where T : class;
        Task<int> AddAsync<T>(T entity) where T : class; // InsertAsync
        Task<int> UpdateAsync<T>(T entity) where T : class;
        Task<int> DeleteAsync<T>(T entity) where T : class;
        Task<int> DeleteAsync<T>(Guid id) where T : class;
    }
}