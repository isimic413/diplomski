using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ExamPreparation.Model;
using ExamPreparation.Model.Common;
using ExamPreparation.Service;
using ExamPreparation.Service.Common;
using ExamPreparation.WebApi.Models;

namespace ExamPreparation.WebApi.Controllers
{
    public class TestingAreaController : ApiController
    {
        private ITestingAreaService Service { get; set; }

        public TestingAreaController(ITestingAreaService service)
        {
            Service = service;
        }
        
        // GET: api/TestingArea
        public IEnumerable<ITestingArea> Get()
        {
            return (IEnumerable<ITestingArea>)Service.GetAll();
        }

        // GET: api/TestingArea/5
        public ITestingArea Get(Guid id)
        {
            return (ITestingArea)Service.GetById(id);
        }

        // POST: api/TestingArea
        public void Post(TestingAreaViewModel testingArea)
        {
            testingArea.Id = Guid.NewGuid();
            Service.Add(AutoMapper.Mapper.Map<TestingArea>(testingArea));
        }

        // PUT: api/TestingArea/5
        public void Put(Guid id, TestingAreaViewModel testingArea)
        {
            if(id == testingArea.Id)
            {
                Service.Update(AutoMapper.Mapper.Map<TestingArea>(testingArea));
            }
        }

        // DELETE: api/TestingArea/5
        public void Delete(Guid id) 
        {
            Service.Delete(id);
        }
    }
}
