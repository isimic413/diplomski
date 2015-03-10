using ExamPreparation.Model.Common;

namespace ExamPreparation.Model.Mapping
{
    public static class AutoMapperMaps
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.TestingArea, ITestingArea>().ReverseMap();
            AutoMapper.Mapper.CreateMap<ExamPreparation.Model.TestingArea, ExamPreparation.DAL.Models.TestingArea>().ReverseMap();
        }

    }
}
