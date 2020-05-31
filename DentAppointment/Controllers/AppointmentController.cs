using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentAppointment.Data;
using DentAppointment.Data.Entity;
using DentAppointment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DentAppointment.Controllers
{
    public class AppointmentController : Controller
    {
        private ApplicationDbContext _dbContext;

        public AppointmentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public JsonResult GetAppointments()
        {
            var model = _dbContext.Appointments.Include(x => x.User).Select(x => new AppointmentViewModel()
            {
                Id= x.Id,
                Color= x.User.Color,
                Dentist = x.User.Name + " " + x.User.Surname,
                PatientName = x.PatientName,
                PatientSurname =  x.PatientSurname,
                StartDate = x.StartDate,
                Description = x.Description,
                EndDate = x.EndDate,
                UserId = x.User.Id
            }); //join appointment with user table (include come with ef core)
            return Json(model);
        }

        public JsonResult GetAppointmentsByDentistId(string userId = "")
        {
            var model = _dbContext.Appointments.Where(x => x.UserId.ToString() == userId).Include(x => x.User).Select(x => new AppointmentViewModel()
            {
                Id= x.Id,
                Color= x.User.Color,
                Dentist = x.User.Name + " " + x.User.Surname,
                PatientName = x.PatientName,
                PatientSurname = x.PatientSurname,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                UserId = x.User.Id
            }); ; //join appointment with user table (include come with ef core)
            return Json(model);
        }

        [HttpPost]
        public JsonResult AddorUpdateAppointment(AddorUpdateAppointment model)
        {
            if (model.Id == 0)
            {
                Appointment entity = new Appointment()
                {
                
                    CreatedDate = DateTime.Now,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    PatientName = model.PatientName,
                    PatientSurname = model.PatientSurname,
                    Description = model.Description,
                    UserId = model.UserId,
                    PatientPhoneNumber = model.PatientPhoneNumber,
                };
                _dbContext.Add(entity);
                _dbContext.SaveChanges();
            }
            else
            {
                var entity = _dbContext.Appointments.SingleOrDefault(x => x.Id == model.Id);
                if (entity == null)
                {
                    return Json("Couldnt found data to update");
                }
                entity.UpdatedDate = DateTime.Now;
                entity.PatientName = model.PatientName;
                entity.PatientSurname = model.PatientSurname;
                entity.Description = model.Description;
                entity.UserId = model.UserId;
                entity.StartDate = model.StartDate;
                entity.EndDate = model.EndDate;
                entity.PatientPhoneNumber = model.PatientPhoneNumber;

                _dbContext.Update(entity);
                _dbContext.SaveChanges();
            }
            return Json("200");

        }

   
        public JsonResult DeleteAppointment(int id = 0)
        {
            var entity = _dbContext.Appointments.SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return Json("Couldnt found data");
            }
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
            return Json("200");
        }
    }
}
