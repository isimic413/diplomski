using System;

namespace ExamPreparation.Repository.Common
{
    public interface IUnitOfWork
    {
        void Commit();
        void Dispose();
    }
}
