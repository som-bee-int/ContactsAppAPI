using System.ComponentModel.DataAnnotations;

namespace ContactsAppAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}
