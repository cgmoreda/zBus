using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace zBus.Models
{
    public class User
    {

        [Key]
        public int User_Id { get; set; }
        public bool Admin { get; set; } = false;

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
        [Required(ErrorMessage = "Enter an Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone Number is Required")]
        [Phone, Column(TypeName = "varchar(11)")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Must be Digits")]
        [MaxLength(11)]
        [MinLength(11, ErrorMessage = "Must Be 11 Digits")]
        [StringLength(11, ErrorMessage = "Must Be 11 Digits")]
        [Display(Name = "Phone Number")]
        public string? Phone_number { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Add Valid Photo")]
        public string? PhotoPhath { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Must be contains 6 characters, and special character.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Column(TypeName = "nvarchar(100)")]
        [MaxLength(100, ErrorMessage = "Maximam 100 charachters")]
        [Required( ErrorMessage="Enter Password")]
        public string? Password { get; set; }
        public virtual ICollection<Trip>? Trips { get; set; }
    }

}

