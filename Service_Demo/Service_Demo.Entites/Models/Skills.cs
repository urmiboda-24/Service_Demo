using Service_Demo.Entites.DataEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Entites.Models
{
    public class Skills : AuditableEntity<long>
    {
        [Required]
        public string SkillName { get; set; } = null!;
        [Required]
        public bool Status { get; set; } = true;
    }
}
