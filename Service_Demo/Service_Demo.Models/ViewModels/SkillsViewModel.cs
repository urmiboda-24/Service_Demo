using Service_Demo.Entites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Service_Demo.Models.ViewModels
{
    public class SkillsViewModel
    {
        public List<Skills> Skills { get; set; }
        [Required]
        public string SkillsName { get; set; }
        [Required]
        public bool? Status { get; set; }
        public long SkillId { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
