using System;

namespace ExamPreparation.DAL.Common
{
    interface IRole
    {
        System.Guid Id { get; set; }
        string Abrv { get; set; }
        string Title { get; set; }
    }
}
