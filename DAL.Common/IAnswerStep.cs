using System;

namespace ExamPreparation.DAL.Common
{
    public interface IAnswerStep
    {
        System.Guid Id { get; set; }
        System.Guid ProblemId { get; set; }
        System.Int16 StepNumber { get; set; }
        System.SByte Points { get; set; }
        string Text { get; set; }
        Boolean HasPicture { get; set; }
    }
}
