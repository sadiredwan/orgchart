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
    [Table("Modules")]
    public class Module  : IBaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }

        public int parent_id { get; set; }

        public String path_url { get; set; }

        public String ControllerName { get; set; }

        public String Action { get; set; }

        public DateTime? created_at { get; set; }

        public int created_by { get; set; }

        public DateTime? modified_at { get; set; }    

        public int modified_by { get; set; }
    }
}