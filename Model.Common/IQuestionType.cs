using System;
using System.Collections.Generic;

namespace ExamPreparation.Model.Common
{
    public interface IQuestionType
    {
        System.Guid Id { get; set; }
        string Title { get; set; }
        string Abrv { get; set; }
        ICollection<IQuestion> Questions { get; set; }
    }
}
