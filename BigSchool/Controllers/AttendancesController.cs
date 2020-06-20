using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using BigSchool.DTOs;

namespace BigSchool.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _DbContext;
        public AttendancesController()
        {
            _DbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_DbContext.Attendances.Any(a=>a.AttendeeId == userId && a.CourseId== attendanceDto.CourseId))
            {
                return BadRequest("The Attendance already exists!");
            }
            var attendance = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = userId
            };
            _DbContext.Attendances.Add(attendance);
            _DbContext.SaveChanges();
            return Ok();
        }
    }
}
