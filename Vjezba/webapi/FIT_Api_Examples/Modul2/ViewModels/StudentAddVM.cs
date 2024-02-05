namespace FIT_Api_Examples.Modul2.ViewModels;

public class StudentAddVM
{
    public int id { get; set; }
    public string ime { get; set; }
    public string prezime { get; set; }
    public int? opstinaId { get; set; }
    public bool? isDeleted { get; set; }
}