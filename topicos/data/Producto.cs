namespace topicos.data
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
    }

    public class PlanDeEstudio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }

        // Relación con Materia (uno a muchos)
        public virtual ICollection<Materia> Materias { get; set; } = new List<Materia>();
    }

    public class Materia
    {
        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Nombre { get; set; }
        public int Creditos { get; set; }

        // Relación con PlanDeEstudio
        public int PlanDeEstudioId { get; set; }
        public virtual PlanDeEstudio PlanDeEstudio { get; set; }

        // Relación con Prerequisito (muchos a muchos)
        public virtual ICollection<Prerequisito> Prerequisitos { get; set; } = new List<Prerequisito>();

        // Relación inversa: Una materia puede ser requisito de muchas otras materias
        public virtual ICollection<Prerequisito> EsRequisitoDe { get; set; } = new List<Prerequisito>();

    }

    public class Prerequisito
    {
        public int Id { get; set; }

        public int MateriaId { get; set; }
        public virtual Materia Materia { get; set; }

        public int MateriaRequisitoId { get; set; }
        public virtual Materia MateriaRequisito { get; set; }
    }


}
