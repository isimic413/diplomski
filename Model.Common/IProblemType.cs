using System;

namespace ExamPreparation.Model.Common
{
    public interface IProblemType
    {
        System.Guid Id { get; set; }
        string Title { get; set; }
        string Abrv { get; set; }
    }
}
