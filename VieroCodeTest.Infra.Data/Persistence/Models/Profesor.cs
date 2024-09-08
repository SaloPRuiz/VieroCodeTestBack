namespace VieroCodeTest.Infra.Data.Persistence.Models;

public class Profesor
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellidos { get; set; }
    public string Genero { get; set; }
    public ICollection<Grado> Grados { get; set; }
}