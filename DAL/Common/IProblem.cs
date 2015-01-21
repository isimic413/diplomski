using System;

namespace ExamPreparation.DAL.Common
{
    interface IProblem
    {
        System.Guid Id { get; set; }
        System.Guid ProblemTypeId { get; set; }
        string Text { get; set; }
        System.SByte Points { get; set; }
        Boolean HasPicture { get; set; }
        Boolean HasSteps { get; set; }
    }
}
