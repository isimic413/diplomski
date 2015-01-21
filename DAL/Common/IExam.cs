using System;

namespace ExamPreparation.DAL.Common
{
    interface IExam
    {
        System.Guid Id { get; set; }
        System.Guid TestingAreaId { get; set; }
        System.Int16 Year { get; set; }
        System.SByte Month { get; set; }
        System.Int16 Duration { get; set; }
    }
}
