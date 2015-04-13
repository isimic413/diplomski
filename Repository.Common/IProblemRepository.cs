using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IProblemRepository
    {
        Task<List<IProblem>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50);
        Task<IProblem> GetAsync(Guid id);
        Task<int> AddAsync(IProblem entity, IProblemPicture picture = null, 
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null);
        Task<int> UpdateAsync(IProblem entity, IProblemPicture picture = null,
            List<IAnswerChoice> choices = null, List<IAnswerChoicePicture> choicePictures = null);
        Task<int> DeleteAsync(IProblem entity);
        Task<int> DeleteAsync(Guid id);
    }
}
