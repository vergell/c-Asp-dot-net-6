using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace WebApp.Models
{
    [Index(nameof(Contacts.Email), IsUnique = true)]
    public class Contacts
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100, MinimumLength = 3)]
        public String? FirstName { get; set; }
        [Required, StringLength(100, MinimumLength = 3)]
        public String? LastName { get; set; }
        [Required, StringLength(100, MinimumLength = 3)]
        public String? Phone { get; set; }
        [Required, StringLength(100, MinimumLength = 3)]
        public String? Email { get; set; }

    }
}
