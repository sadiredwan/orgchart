using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace orgchart.Models 
{
    [Table("designations")]
    public class Designation :IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public String name { get; set; }

        [Required]
        public String short_name { get; set; }
        public String remarks { get; set; }
        public int parent_id { get; set; }
        public DateTime? created_at { get; set; }

        public DateTime? modified_at { get; set; }

        public int created_by { get; set; }

        public int modified_by { get; set; }
        public int company_id { get; set; }
    }
}