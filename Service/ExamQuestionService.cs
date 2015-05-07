﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExamPreparation.Common.Filters;
using ExamPreparation.Model.Common;
using ExamPreparation.Repository.Common;
using ExamPreparation.Service.Common;

namespace ExamPreparation.Service
{
    public class ExamQuestionService: IExamQuestionService
    {
        #region Properties

        protected IExamQuestionRepository Repository { get; private set; }

        #endregion Properties

        #region Constructors

        public ExamQuestionService(IExamQuestionRepository repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        #region Get

        public Task<List<IExamQuestion>> GetAsync(ExamQuestionFilter filter)
        {
            try
            {
                return Repository.GetAsync(filter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<IExamQuestion> GetAsync(Guid id)
        {
            try
            {
                return Repository.GetAsync(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<List<IQuestion>> GetExamQuestionsAsync(Guid examId)
        {
            try
            {
                return Repository.GetExamQuestionsAsync(examId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<IQuestion> GetQuestionAsync(Guid examId, int questionNumber)
        {
            try
            {
                return Repository.GetQuestionAsync(examId, questionNumber);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Get

        #region Insert

        public Task<int> InsertAsync(IExamQuestion entity)
        {
            try
            {
                return Repository.InsertAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Insert

        #region Update

        public Task<int> UpdateAsync(IExamQuestion entity)
        {
            try
            {
                return Repository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Update

        #region Delete

        public Task<int> DeleteAsync(IExamQuestion entity)
        {
            try
            {
                return Repository.DeleteAsync(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return Repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Delete

        #endregion Methods
    }
}