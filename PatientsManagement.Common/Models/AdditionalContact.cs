using System.ComponentModel.DataAnnotations;
using PatientsManagement.Common.Helpers;

namespace PatientsManagement.Common.Models
{
    public class AdditionalContact
    {
        public int Id { get; set; }

        public ContactKind Kind { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13), RegularExpression(Formats.PhoneRegex)]
        public string Phone { get; set; }
    }
}
