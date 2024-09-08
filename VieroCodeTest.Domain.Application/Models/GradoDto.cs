namespace VieroCodeTest.Domain.Application.Models;

public class GradoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int ProfesorId { get; set; }
    public string ProfesorNombre { get; set; } = "";
}