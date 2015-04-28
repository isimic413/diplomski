using System;
using System.Collections.Generic;

namespace ExamPreparation.Model.Common
{
    public interface IExam
    {
        System.Guid Id { get; set; }
        System.Guid TestingAreaId { get; set; }
        short Year { get; set; }
        short Month { get; set; }
        System.TimeSpan Duration { get; set; }
        ITestingArea TestingArea { get; set; }
        ICollection<IExamQuestion> ExamQuestions { get; set; }
    }
}
