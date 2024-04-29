using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace zBus.Models
{
    public class User
    {

        [Key]
        public int User_Id { get; set; }


        [Required(ErrorMessage = "Required Feild")]
        [MinLength(3, ErrorMessage = "At least 3 charachters")]
        [MaxLength(20, ErrorMessage = "maximam 20 charachters")]
        [Display(Name = "First Name")]
        public string Fisrt_name { get; set; }



        [Required(ErrorMessage = "Required Feild")]
        [MinLength(3, ErrorMessage = "At least 3 charachters")]
        [MaxLength(20, ErrorMessage = "maximam 20 charachters")]
        [Display(Name = "Last Name")]
        public string Last_name { get; set; }



        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }



        [Phone, Column(TypeName = "varchar(11)")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Must be Digits")]
        [MaxLength(11)]
        [StringLength(11, ErrorMessage = "Must Be 11 Digits")]
        [Display(Name = "Phone Number")]
        public string? Phone_number { get; set; }

        [Display(Name = "Image")]
        public string? PhotoPhath { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Column(TypeName = "nvarchar(100)")]
        [MaxLength(100, ErrorMessage = "Maximam 100 charachters")]
        [Required]
        public string? Password { get; set; }

        public virtual ICollection<Trip>? Trips { get; set; }
    }

}

