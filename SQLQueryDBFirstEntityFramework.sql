CREATE DATABASE DBFirstEntityFramework

CREATE TABLE aluno
(
	id integer identity primary key,
	nome varchar(100) not null
)

CREATE TABLE curso
(
	id integer identity primary key,
	nome varchar(100) not null
)

CREATE TABLE matricula -- tabela associativa
(
	registro integer identity primary key,
	fk_alunoId integer,
	foreign key (fk_alunoId) references aluno(id),
	fk_cursoId integer,
	foreign key (fk_cursoId) references curso(id)
)

select * from curso
select * from aluno

SELECT m.registro, a.nome AS nome_aluno, c.nome AS nome_curso
FROM matricula m
JOIN aluno a ON m.fk_alunoId = a.id
JOIN curso c ON m.fk_cursoId = c.id;

-- consulta um aluno especifico pelo id
SELECT a.nome AS nome_aluno, c.nome AS nome_curso
FROM matricula m
JOIN aluno a ON m.fk_alunoId = a.id
JOIN curso c ON m.fk_cursoId = c.id
WHERE a.id = 1;

-- consulta todos os alunos e seus id e cursos matriculados
SELECT a.id, a.nome AS nome_aluno, c.nome AS nome_curso
FROM aluno a
LEFT JOIN matricula m ON m.fk_alunoId = a.id
LEFT JOIN curso c ON m.fk_cursoId = c.id;
