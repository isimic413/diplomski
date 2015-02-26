using System;
using System.ComponentModel.DataAnnotations;

namespace ExamPreparation.WebApi.Models
{
    public class TestingAreaViewModel
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Testing area title")]
        public string Title { get; set; }

        [Display(Name = "Testing area abbreviation")]
        public string Abrv { get; set; }
    }
}