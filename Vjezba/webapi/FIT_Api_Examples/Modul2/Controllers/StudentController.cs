using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Helper;
using FIT_Api_Examples.Helper.AutentifikacijaAutorizacija;
using FIT_Api_Examples.Modul2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul2.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

      

        [HttpGet]
        public ActionResult<List<Student>> GetAll(string ime_prezime,string opstina)
        {
            var data = _dbContext.Student
                .Include(s => s.opstina_rodjenja.drzava)
                .Where(x => (ime_prezime == null ||
                             (x.ime + " " + x.prezime).ToLower().StartsWith(ime_prezime.ToLower()) || 
                             (x.prezime + " " + x.ime).ToLower().StartsWith(ime_prezime.ToLower())) &&
                            (opstina == null || x.opstina_rodjenja.description.ToLower().StartsWith(opstina.ToLower()))&& x.isDeleted==false)
                .OrderByDescending(s => s.id)
                .AsQueryable();
            return data.Take(100).ToList();
        }

        [HttpPut]
        public ActionResult EditStudent(StudentAddVM s)
        {
            var student = _dbContext.Student.Find(s.id);
            student.ime = s.ime;
            student.prezime = s.prezime;
            student.opstina_rodjenja_id = s.opstinaId;
            _dbContext.SaveChanges();
            return Ok(student);
        }

        [HttpDelete]
        public ActionResult SoftDelete(StudentAddVM s)
        {
            var student = _dbContext.Student.Find(s.id);
            student.isDeleted = s.isDeleted;
            _dbContext.SaveChanges();
            return Ok(student);
        }

        [HttpGet]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = _dbContext.Student.Find(id);
            return student;
        }
        [HttpPost]
        public ActionResult AddStudent(StudentAddVM s)
        {
            var student = new Student
            {
                ime = s.ime,
                prezime = s.prezime,
                opstina_rodjenja_id = s.opstinaId
            };
            _dbContext.Student.Add(student);
            _dbContext.SaveChanges();
            return Ok(student);
        }

    }
}
