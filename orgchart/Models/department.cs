//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace orgchart.Models
{
    [Table("departments")]
    public class Department : IBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [DepartmentNameValidation(ErrorMessage = "Department already exists")]
        public string name { get; set; }

        public string description { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? modified_at { get; set; }

        public int created_by { get; set; }

        public int modified_by { get; set; }

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
        public class DepartmentNameValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (!(value is string)) return new ValidationResult("DataType Error");
                var val = (string)value;
                try
                {
                    var idProperty = validationContext.ObjectInstance.GetType().GetProperty("id");
                    var idPropertyValue = idProperty.GetValue(validationContext.ObjectInstance, null);
                    if (idPropertyValue.ToString() != "0")
                    {
                        return ValidationResult.Success;
                    }
                    var findDepartmentName = new DBContext().departments.ToList().FindAll(x => x.name == val);
                    if (findDepartmentName.Count > 0)
                    {
                        return new ValidationResult(FormatErrorMessage(validationContext.MemberName));
                    }
                }
                catch (Exception e)
                {
                    return ValidationResult.Success;
                }
                return ValidationResult.Success;
            }
            public override string FormatErrorMessage(string name)
            {
                return base.FormatErrorMessage(name);
            }
        }

    }
}
