using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientsManagement.Common.Helpers
{
    static class Formats
    {
        public const string CyrillicSpaceOrDash = @"^[\p{IsCyrillic} -]+$";
        public const string PhoneRegex = @"^\+[0-9]{12}$";
    }
}
