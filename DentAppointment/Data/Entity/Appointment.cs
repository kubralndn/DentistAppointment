using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentAppointment.Data.Entity
{
    public class Appointment
    {
        public int UserId { get; set; }//foreign Key
        public AppUser User { get; set; } //navigation property
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string Description { get; set; }
    }
}
