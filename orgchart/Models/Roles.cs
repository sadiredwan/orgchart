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
    [Table("Roles")]
    public class Roles : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public String name { get; set; }

        public DateTime? created_at { get; set; }

        public int created_by { get; set; }

        public DateTime? modified_at { get; set; }

        public int modified_by { get; set; }
    }
}