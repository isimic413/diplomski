using System;
using System.Collections.Generic;

namespace ExamPreparation.Model.Common
{
    public interface ITestingArea
    {
        System.Guid Id { get; set; }
        string Title { get; set; }
        string Abrv { get; set; }
        ICollection<IExam> Exams { get; set; }
        ICollection<IQuestion> Questions { get; set; }
    }
}
