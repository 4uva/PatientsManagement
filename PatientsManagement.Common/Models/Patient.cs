using PatientsManagement.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatientsManagement.Common.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [StringLength(128, MinimumLength = 1)]
        [Required]
        [RegularExpression(Formats.CyrillicSpaceOrDash)]
        public string Surname { get; set; }

        [StringLength(128, MinimumLength = 1)]
        [Required]
        [RegularExpression(Formats.CyrillicSpaceOrDash)]
        public string Name { get; set; }

        [StringLength(128, MinimumLength = 1)]
        [RegularExpression(Formats.CyrillicSpaceOrDash)]
        public string Patronymic { get; set; }

        [Required]
        [ValidDate]
        DateTime DateOfBirth { get; set; }

        Gender Gender { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13), RegularExpression(Formats.PhoneRegex)]
        public string PrimaryPhone { get; set; }

        public List<AdditionalContact> AdditionalContacts { get; set; }
    }
}