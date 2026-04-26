namespace SistemaPrestamos;

public abstract class Material
{
    public int    Id          { get; set; }
    public string Nombre      { get; protected set; }
    public string Codigo      { get; protected set; }
    public bool   Disponible  { get; set; }
    public string Propietario { get; protected set; }

    protected Material(string nombre, string codigo, string propietario)
    {
        Nombre      = nombre;
        Codigo      = codigo;
        Propietario = propietario;
        Disponible  = true;
    }

    public abstract string ObtenerTipo();
    public abstract string ObtenerDescripcion();

    public virtual string ObtenerEstado() => Disponible ? " Disponible" : " Prestado";

    public override string ToString()
        => $"[{ObtenerTipo()}] {Nombre} (Cód: {Codigo}) | {ObtenerEstado()}";

    public override bool Equals(object? obj) => obj is Material m && m.Codigo == Codigo;
    public override int  GetHashCode()        => Codigo.GetHashCode();
}
