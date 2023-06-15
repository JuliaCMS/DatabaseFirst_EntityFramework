namespace DBFirstEntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Digite:\n" +
                "1 - para cadastrar um novo curso\n" +
                "2 - para cadastrar um novo aluno\n" +
                "3 - para adicionar novo curso ao aluno\n" +
                "4 - para excluir um aluno\n" +
                "5 - para excluir um curso\n" +
                "6 - para consultar aluno pela matricula\n" +
                "7 - para listar todos os alunos\n" +
                "8 - para listar todos os cursos\n" +
                "9 - para listar relação entre cursos e alunos\n" +
                "0 - para encerrar");
                int op = int.Parse(Console.ReadLine());

                DbfirstEntityFrameworkContext context = new DbfirstEntityFrameworkContext();

                switch (op)
                {
                    case 0:
                        break;
                    case 1:
                        try
                        {
                            Console.WriteLine("Insira o nome do curso:");
                            Curso curso = new Curso();
                            curso.Nome = Console.ReadLine();

                            context.Cursos.Add(curso);
                            context.SaveChanges();
                            Console.WriteLine("Curso criado com sucesso.");
                            
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.WriteLine("Insira o nome do aluno:");
                            string nomeAluno = Console.ReadLine();

                            Console.WriteLine("Insira o nome do curso que o aluno será matriculado:");
                            string nomeCurso = Console.ReadLine();

                            // Verifica se o aluno já existe no banco de dados
                            Aluno aluno = context.Alunos.FirstOrDefault(a => a.Nome == nomeAluno);

                            if (aluno == null)
                            {
                                // Se o aluno não existe, cria um novo aluno
                                aluno = new Aluno { Nome = nomeAluno };
                                context.Alunos.Add(aluno);
                            }

                            // Verifica se o curso já existe no banco de dados
                            Curso curso = context.Cursos.FirstOrDefault(c => c.Nome == nomeCurso);

                            if (curso == null)
                            {
                                // Se o curso não existe, cria um novo curso
                                curso = new Curso { Nome = nomeCurso };
                                context.Cursos.Add(curso);
                            }

                            // Cria uma nova matrícula e associa o aluno e o curso
                            Matricula matricula = new Matricula { FkAluno = aluno, FkCurso = curso };
                            context.Matriculas.Add(matricula);

                            context.SaveChanges();
                            Console.WriteLine("Aluno cadastrado e matriculado com sucesso.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                }
            } while (true);
        }
    }
}