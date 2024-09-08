namespace VieroCodeTest.Infra.Data.Persistence.Models;

public class AlumnoGrado
{
    public int Id { get; set; }

    // Foreign Key para Alumno
    public int AlumnoId { get; set; }
    public Alumno Alumno { get; set; }

    // Foreign Key para Grado
    public int GradoId { get; set; }
    public Grado Grado { get; set; }

    public string Seccion { get; set; }
}