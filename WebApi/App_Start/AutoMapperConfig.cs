using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ExamPreparation.Model;
using ExamPreparation.WebApi.Controllers;

namespace ExamPreparation.WebApi.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            ExamPreparation.Model.Mapping.AutoMapperMaps.Initialize();

            AutoMapper.Mapper.CreateMap<TestingAreaModel, TestingArea>().ReverseMap();
        }
    }
}