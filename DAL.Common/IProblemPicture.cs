using System;

namespace ExamPreparation.DAL.Common
{
    public interface IProblemPicture
    {
        System.Guid Id { get; set; }
        System.Guid ProblemId { get; set; }
        byte[] Picture { get; set; }
    }
}
