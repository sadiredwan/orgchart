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
    [Table("employees")]
    public class Employee : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int supervisor_id { get; set; }

        public String test { get; set; }

        public String national_idcard { get; set; }

        [Required]
        public String first_name { get; set; }

        [Required]
        public String last_name { get; set; }

        [Required]
        public String email { get; set; }

        public String contact_no { get; set; }

        public String contact_address { get; set; }

        public String photo { get; set; }

        public int designation_id { get; set; }

        public int company_id { get; set; }

        public int department_id { get; set; }

        [Required]
        public DateTime? joining_date { get; set; }

        //[ForeignKey("company_id")]
        //public virtual company company { get; set; }

        //[ForeignKey("designation_id")]
        //public virtual Designation designation { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? modified_at { get; set; }

        public int created_by { get; set; }

        public int modified_by { get; set; }


    }
}