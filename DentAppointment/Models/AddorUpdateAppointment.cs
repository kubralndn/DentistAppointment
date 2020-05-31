using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentAppointment.Models
{
    public class AddorUpdateAppointment
    {
        public string UserId { get; set; }//foreign Key
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string Description { get; set; }
    }
}
