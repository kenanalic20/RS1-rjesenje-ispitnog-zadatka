using System;
using System.ComponentModel.DataAnnotations.Schema;
using FIT_Api_Examples.Data;

namespace FIT_Api_Examples.Modul2.ViewModels;

public class MaticnaKnjigaAddVM
{
    public int? id { get; set; }
    public DateTime? datumUpisa { get; set; }
    public int? godinaStudija { get; set; }
    public float? cijenaSkolarine { get; set; }
    public bool? obnova { get; set; }
    public DateTime? datumOvjere { get; set; }
    public string? Napomena { get; set; }
    public int? akademskaGodinaId { get; set; }
    public int? studentId { get; set; }
    public string? evidentirao { get; set; }
 
}