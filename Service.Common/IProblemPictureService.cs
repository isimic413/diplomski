using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IProblemPictureService
    {
        Task<List<IProblemPicture>> GetAsync(string sortOrder = "problemId", int pageNumber = 0, int pageSize = 50);
        Task<IProblemPicture> GetAsync(Guid id);
        Task<int> AddAsync(IProblemPicture entity);
        Task<int> UpdateAsync(IProblemPicture entity);
        Task<int> DeleteAsync(IProblemPicture entity);
        Task<int> DeleteAsync(Guid id);
    }
}
