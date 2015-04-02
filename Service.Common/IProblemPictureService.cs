using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Service.Common
{
    public interface IProblemPictureService
    {
        Task<List<IProblemPicture>> GetPageAsync(int pageSize, int pageNumber);

        Task<List<IProblemPicture>> GetAllAsync();
        Task<IProblemPicture> GetByIdAsync(Guid id);
        Task<int> AddAsync(IProblemPicture entity);
        Task<int> UpdateAsync(IProblemPicture entity);
        Task<int> DeleteAsync(IProblemPicture entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddUoWAsync(IProblemPicture entity);
    }
}
