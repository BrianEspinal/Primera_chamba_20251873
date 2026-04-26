// ============================================================
//  CLASES: Cuaderno, Libro, Herramienta
// ============================================================
using System;

namespace SistemaPrestamos
{
    // ── Cuaderno ───────────────────────────────────────────────
    public class Cuaderno : Material
    {
        public int    NumeroPaginas { get; private set; }
        public string Materia       { get; private set; }

        // Constructor completo
        public Cuaderno(string nombre, string codigo, string propietario,
                        string materia, int paginas)
            : base(nombre, codigo, propietario)
        {
            Materia       = materia;
            NumeroPaginas = paginas;
        }

        // Constructor sobrecargado (sin páginas → default 100)
        public Cuaderno(string nombre, string codigo, string propietario, string materia)
            : this(nombre, codigo, propietario, materia, 100) { }

        public override string ObtenerTipo()         => "CUADERNO";
        public override string ObtenerDescripcion()  =>
            $"Materia: {Materia} | Páginas: {NumeroPaginas}";

        public override string ObtenerEstado()
        {
            string base_ = base.ObtenerEstado();
            return Disponible ? base_ : $"{base_} ⚠";
        }
    }

    // ── Libro ──────────────────────────────────────────────────
    public class Libro : Material
    {
        public string Autor   { get; private set; }
        public string Edition { get; private set; }

        public Libro(string nombre, string codigo, string propietario,
                     string autor, string edicion = "1ra")
            : base(nombre, codigo, propietario)
        {
            Autor   = autor;
            Edition = edicion;
        }

        public override string ObtenerTipo()        => "LIBRO";
        public override string ObtenerDescripcion() =>
            $"Autor: {Autor} | Edición: {Edition}";
    }

    // ── Herramienta / Utensilio ────────────────────────────────
    public class Herramienta : Material
    {
        public string Categoria { get; private set; }

        public Herramienta(string nombre, string codigo, string propietario,
                           string categoria)
            : base(nombre, codigo, propietario)
        {
            Categoria = categoria;
        }

        public override string ObtenerTipo()        => "HERRAMIENTA";
        public override string ObtenerDescripcion() =>
            $"Categoría: {Categoria}";
    }
}
