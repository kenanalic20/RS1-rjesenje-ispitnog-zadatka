using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIT_Api_Examples.Data;

public class MaticnaKnjiga
{
    [Key]
    public int id { get; set; }
    public DateTime? datumUpisa { get; set; }
    public int? godinaStudija { get; set; }
    public float? cijenaSkolarine { get; set; }
    public bool? obnova { get; set; }
    public DateTime? datumOvjere { get; set; }
    public string? Napomena { get; set; }
     [ForeignKey(nameof(akademskaGodina))]
    public int? akademskaGodinaId { get; set; }
    public AkademskaGodina akademskaGodina { get; set; }

    [ForeignKey(nameof(Student))] 
    public int? studentId { get; set; }

    public Student? Student { get; set; }

    public string? evidentirao { get; set; }
    
    
}