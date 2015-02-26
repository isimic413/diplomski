using System;

namespace ExamPreparation.Model.Common
{
    public interface ITestingArea
    {
        System.Guid Id { get; set; }
        string Title { get; set; }
        string Abrv { get; set; }
    }
}
