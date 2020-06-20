using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BigSchool.Models;
using Microsoft.AspNet.Identity;

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
        public IHttpActionResult Attend([FromBody] int CourseId)
        {
            var attendance = new Attendance
            {
                CourseId = CourseId,
                AttendeeId = User.Identity.GetUserId()
            };
            _DbContext.Attendances.Add(attendance);
            _DbContext.SaveChanges();
            return Ok();
        }
    }
}
