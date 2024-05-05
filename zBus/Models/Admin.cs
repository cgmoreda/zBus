using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace zBus.Models
{
    public class Admin
    {

        [Key]
        public int Admin_Id { get; set; }


        [Required(ErrorMessage = "Required Feild")]
        [MinLength(3, ErrorMessage = "At least 3 charachters")]
        [MaxLength(20, ErrorMessage = "maximam 20 charachters")]
        [Display(Name = "First Name")]
        public string? Fisrt_name { get; set; }



        [Required(ErrorMessage = "Required Feild")]
        [MinLength(3, ErrorMessage = "At least 3 charachters")]
        [MaxLength(20, ErrorMessage = "maximam 20 charachters")]
        [Display(Name = "Last Name")]
        public string? Last_name { get; set; }



        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }



        [Phone, Column(TypeName = "varchar(11)")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(11)]
        [MinLength(11, ErrorMessage = "Must Be 11 Digits")]
        [Display(Name = "Phone Number")]
        public string? Phone_number { get; set; }



        [Column(TypeName = "nvarchar(255)")]
        [Display(Name = "Address")]
        public string? Address { get; set; }



        // [RegularExpression(@"^()",ErrorMessage = "Password must contain at least one digit and one special character.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Column(TypeName = "nvarchar(100)")]
        [MaxLength(100, ErrorMessage = "Maximam 100 charachters")]
        public string? Password { get; set; }


        [Required(ErrorMessage = "Enter Birth Date")]
        [Display(Name = "Birth Date")]
        // [Range(typeof(DateTime), "1950-01-01", "2100-01-01", ErrorMessage = "Invalid date.")]
        [DataType(DataType.Date)]
        public DateTime Birth_date { get; set; }
    }
}
