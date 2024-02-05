using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using FIT_Api_Examples.Data;
using FIT_Api_Examples.Modul2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FIT_Api_Examples.Modul2.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class MaticnaController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public MaticnaController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<List<MaticnaKnjiga>> GetAllMaticna(int studentId)
    {
        var data = _dbContext.MaticnaKnjiga.Where(x => x.studentId == studentId).Include(x=>x.akademskaGodina).ToList();
        return Ok(data);
    }

    [HttpPut]
    public ActionResult<MaticnaKnjiga> EditMaticna(MaticnaKnjigaAddVM m)
    {
        var data = _dbContext.MaticnaKnjiga.Find(m.id);
        data.Napomena = m.Napomena;
        data.datumOvjere = m.datumOvjere;
        _dbContext.SaveChanges();
        return Ok();
    }
    [HttpPost]
    public ActionResult<MaticnaKnjiga> AddMaticna(MaticnaKnjigaAddVM m)
    {
        var studentData = _dbContext.MaticnaKnjiga.Where(x => x.studentId == m.studentId).ToList();

        if (m.obnova == false)
        {
            foreach (var sd in studentData)
            {
                if (m.godinaStudija == sd.godinaStudija)
                {
                    return BadRequest("Molimo oznacite obnovu");
                }
            }
        }
         var maticna = new MaticnaKnjiga
         {
             datumUpisa = m.datumUpisa,
            akademskaGodinaId = m.akademskaGodinaId,
            godinaStudija = m.godinaStudija,
            cijenaSkolarine = m.cijenaSkolarine,
            obnova = m.obnova,
            evidentirao = m.evidentirao,
            studentId = m.studentId
         };
         _dbContext.MaticnaKnjiga.Add(maticna);
         _dbContext.SaveChanges();
        
        return Ok(maticna);
    }

}