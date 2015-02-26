using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPreparation.WebApi.Models
{
    public class TestingAreaListViewModel
    {
        public IEnumerable<TestingAreaViewModel> TestingAreas { get; set; }
    }
}