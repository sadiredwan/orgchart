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
    [Table("RolePermissions")]
    public class RolePermission : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int role_id { get; set; }

        public int module_id { get; set; }

        public bool Is_View { get; set; }

        public bool Is_Create { get; set; }

        public bool Is_Delete { get; set; }

        public bool Is_Update { get; set; }

        [ForeignKey("role_id")]
        public Roles role { get; set; }

        [ForeignKey("module_id")]
        public Module module { get; set; }

        public DateTime? created_at { get; set; }

        public int created_by { get; set; }

        public DateTime? modified_at { get; set; }

        public int modified_by { get; set; }
    }
}