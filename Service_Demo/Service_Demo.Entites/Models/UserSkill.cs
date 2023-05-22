using Service_Demo.Entites.DataEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_Demo.Entites.Models
{
    public class UserSkill : AuditableEntity<long>
    {
        public long UserId { get; set; }
        public long SkillId { get; set; }
        [ForeignKey("SkillId")]
        public Skills Skill { get; set; } = null!;
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
