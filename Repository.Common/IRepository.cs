﻿using System;
using System.Linq;

namespace ExamPreparation.Repository.Common
{
    public interface IRepository
    {
        IQueryable<T> GetAll<T>() where T: class;
        T GetById<T>(Guid id) where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Delete<T>(Guid id) where T : class;
    }
}