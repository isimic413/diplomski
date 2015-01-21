using System;

namespace ExamPreparation.DAL.Common
{
    interface IProblemType
    {
        System.Guid Id { get; set; }
        string Title { get; set; }
        string Abrv { get; set; }
    }
}
