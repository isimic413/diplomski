using System;
using ExamPreparation.DAL.Models;

namespace ExamPreparation.Repository.Common
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork();
    }
}
