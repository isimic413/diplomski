using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPreparation.Model.Common;

namespace ExamPreparation.Repository.Common
{
    public interface IProblemPictureRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        void CreateUnitOfWork();

        Task<List<IProblemPicture>> GetPageAsync(int pageSize = 0, int pageNumber = 0);

        Task<List<IProblemPicture>> GetAllAsync();
        Task<IProblemPicture> GetByIdAsync(Guid id);
        Task<int> AddAsync(IProblemPicture entity);
        Task<int> UpdateAsync(IProblemPicture entity);
        Task<int> DeleteAsync(IProblemPicture entity);
        Task<int> DeleteAsync(Guid id);

        Task<int> AddAsync(IUnitOfWork unitOfWork, IProblemPicture entity);
    }
}
