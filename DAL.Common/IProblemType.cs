using System;

namespace ExamPreparation.DAL.Common
{
    public interface IProblemType
    {
        System.Guid Id { get; set; }
        string Title { get; set; }
        string Abrv { get; set; }
    }
}
