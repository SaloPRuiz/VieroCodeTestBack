namespace VieroCodeTest.Infra.Data.Persistence.Models;

public class Alumno
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string Genero { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public ICollection<AlumnoGrado> AlumnoGrados { get; set; }
}