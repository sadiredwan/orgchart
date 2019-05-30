using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace orgchart.Models
{
    [Table("UserRoles")]
    public class UserRole :IBaseModel
    {
        [Key]
        public int Id { get; set; }

        [Column("rl_id")]
        public int role_id { get; set; }


        public int user_id { get; set; }

        [ForeignKey("role_id")]
        public Roles roles { get; set; }


        [ForeignKey("user_id")]
        public User users { get; set; }

        public DateTime? created_at { get; set; }

        public int created_by { get; set; }

        public DateTime? modified_at { get; set; }

        public int modified_by { get; set; }
    }
}