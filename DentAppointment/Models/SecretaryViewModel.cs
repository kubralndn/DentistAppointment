using DentAppointment.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentAppointment.Models
{
    public class SecretaryViewModel
    {
        public AppUser User { get; set; }

        public IEnumerable<AppUser> Dentists { get; set; }
    }
}
