using Microsoft.EntityFrameworkCore;

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
                "4 - para excluir um curso\n" +
                "5 - para excluir um aluno\n" +
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
                        Console.WriteLine("Programa encerrado!");
                        return;
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

                            // Verifica se o curso já existe no banco de dados
                            Curso curso = context.Cursos.FirstOrDefault(c => c.Nome == nomeCurso);

                            if (curso == null)
                            {
                                Console.WriteLine("Curso não encontrado!");
                            }
                            else
                            {
                                Aluno aluno = new Aluno { Nome = nomeAluno };
                                context.Alunos.Add(aluno);

                                Matricula matricula = new Matricula { FkAluno = aluno, FkCurso = curso };
                                context.Matriculas.Add(matricula);

                                context.SaveChanges();

                                Console.WriteLine("Aluno cadastrado e matriculado com sucesso.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Informe o ID do aluno:");
                            int id = int.Parse(Console.ReadLine());
                            Aluno aluno = context.Alunos.Find(id);

                            Console.WriteLine("Informe o novo curso que o aluno será matriculado:");
                            string nomeCurso = Console.ReadLine();

                            Curso curso = context.Cursos.FirstOrDefault(c => c.Nome == nomeCurso);

                            if (curso == null)
                            {
                                Console.WriteLine("Curso não encontrado!");
                            }
                            else
                            {
                                Matricula matricula = new Matricula { FkAluno = aluno, FkCurso = curso };
                                context.Matriculas.Add(matricula);
                                context.Alunos.Update(aluno);
                                context.SaveChanges();

                                Console.WriteLine("Novo curso adicionado ao aluno com sucesso.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 4:
                        try
                        {
                            Console.WriteLine("Informe o ID do curso para exclusão: ");
                            int idCurso = int.Parse(Console.ReadLine());
                            Curso curso = context.Cursos.Include(c => c.Matriculas).FirstOrDefault(c => c.Id == idCurso);

                            if (curso != null)
                            {
                                Console.WriteLine($"Confirmar a exclusão de {curso.Nome}?\nDigite [1] para sim e [2] para não");
                                if (int.Parse(Console.ReadLine()) == 1)
                                {
                                    // Remove as matrículas associadas ao curso
                                    foreach (Matricula matricula in curso.Matriculas.ToList())
                                    {
                                        context.Matriculas.Remove(matricula);
                                    }

                                    // Remove o curso
                                    context.Cursos.Remove(curso);
                                    context.SaveChanges();
                                    Console.WriteLine("Curso excluído com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Exclusão cancelada.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Curso não encontrado!");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 5:
                        try
                        {
                            Console.WriteLine("Informe o ID do aluno para exclusão: ");
                            int idAluno = int.Parse(Console.ReadLine());
                            Aluno aluno = context.Alunos.Include(a => a.Matriculas).FirstOrDefault(a => a.Id == idAluno);

                            if (aluno != null)
                            {
                                Console.WriteLine($"Confirmar a exclusão do aluno {aluno.Nome}?\nDigite [1] para sim e [2] para não");
                                if (int.Parse(Console.ReadLine()) == 1)
                                {
                                    // Remove as matrículas associadas ao aluno
                                    foreach (Matricula matricula in aluno.Matriculas.ToList())
                                    {
                                        context.Matriculas.Remove(matricula);
                                    }

                                    // Remove o curso
                                    context.Alunos.Remove(aluno);
                                    context.SaveChanges();
                                    Console.WriteLine("Aluno excluído com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Exclusão cancelada.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Curso não encontrado!");
                            }
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