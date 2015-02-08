using System;

namespace ExamPreparation.DAL.Common
{
    public interface IRole
    {
        System.Guid Id { get; set; }
        string Abrv { get; set; }
        string Title { get; set; }
    }
}
