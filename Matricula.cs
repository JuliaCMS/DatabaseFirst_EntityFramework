using System;
using System.Collections.Generic;

namespace DBFirstEntityFramework;

public partial class Matricula
{
    public int Registro { get; set; }

    public int? FkAlunoId { get; set; }

    public int? FkCursoId { get; set; }

    public virtual Aluno? FkAluno { get; set; }

    public virtual Curso? FkCurso { get; set; }
}
