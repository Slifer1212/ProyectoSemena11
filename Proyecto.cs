class Proyecto
{
    public string Nombre { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<string> Tareas { get; set; }

    public Proyecto(string nombre)
    {
        Nombre = nombre;
        FechaCreacion = DateTime.Now;
        Tareas = new List<string>();
    }
}