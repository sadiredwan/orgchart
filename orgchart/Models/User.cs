using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace orgchart.Models
{
    [Table("Users")]
    public class User : IBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [EmailValidation(ErrorMessage = "Email already exists")]
        public String email { get; set; }

        [Required]
        public String password { get; set; }
        
        [NotMapped]
        [Compare("password", ErrorMessage ="Passwrod doesnot match")]

        public String confirm_password { get; set; }

        public String first_name { get; set; }

        public String last_name { get; set; }        [NotMapped]        public String Company_Name { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? modified_at { get; set; }

        public int created_by { get; set; }

        public int modified_by { get; set; }

    }

    public class LoginUser
    {
        
        [Required]
        public String email { get; set; }

        [Required]
        public String password { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EmailValidation : ValidationAttribute
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
                var findEmail = new DBContext().users.ToList().FindAll(x => x.email == val);
                if (findEmail.Count > 0)
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