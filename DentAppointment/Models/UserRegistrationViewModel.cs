using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentAppointment.Models
{
    public class UserRegistrationViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CityId { get; set; }
    }
}
