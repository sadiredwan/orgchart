using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace orgchart.Models
{
    public interface IBaseModel
    {
        DateTime? created_at { get; set; }
        int created_by { get; set; }
        DateTime? modified_at { get; set; }
        int modified_by { get; set; }

    }
}