using System;
using System.Collections.Generic;

namespace DBFirstEntityFramework.DataModels;

public partial class Curso
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
