namespace VieroCodeTest.Domain.Application.Models;

public class AlumnoGradoDto
{
    public int Id { get; set; }
    public int AlumnoId { get; set; }
    public string AlumnoNombres { get; set; } = "";
    public int GradoId { get; set; }
    public string GradoNombre { get; set; } = "";
    public string Seccion { get; set; } = "";
}