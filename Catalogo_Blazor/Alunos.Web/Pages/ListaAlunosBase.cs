using Alunos.Model;
using Microsoft.AspNetCore.Components;

namespace Alunos.Web.Pages;

public class ListaAlunosBase : ComponentBase
{

    public IEnumerable<Aluno> Alunos { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await Task.Run(LoadAlunos);
    }

    public void LoadAlunos()
    {
        Thread.Sleep(3000);

        Aluno aluno1 = new Aluno
        {
            AlunoId = 1,
            Nome = "Carlos",
            Sobrenome = "Bueno",
            Email = "carlosbueno@msn.com",
            Nascimento = new DateTime(1990, 10, 5),
            Genero = Genero.Masculino,
            Curso = new Curso { CursoId = 1, Nome = "Quimica Inorganica I", Creditos = 40 },
            Foto = "images/foto1.jpg"
        };
        Aluno aluno2 = new Aluno
        {
            AlunoId = 2,
            Nome = "Maria",
            Sobrenome = "Santos",
            Email = "mariasantos@bol.com",
            Nascimento = new DateTime(1992, 6, 7),
            Genero = Genero.Feminino,
            Curso = new Curso { CursoId = 2, Nome = "Literatura I", Creditos = 30 },
            Foto = "images/foto2.jpg"
        };
        Aluno aluno3 = new Aluno
        {
            AlunoId = 3,
            Nome = "Madalena",
            Sobrenome = "Siqueira",
            Email = "madaseiqueira@uol.com",
            Nascimento = new DateTime(1994, 3, 9),
            Genero = Genero.Feminino,
            Curso = new Curso { CursoId = 3, Nome = "Artes Cênicas", Creditos = 20 },
            Foto = "images/foto3.jpg"
        };
        Aluno aluno4 = new Aluno
        {
            AlunoId = 4,
            Nome = "Armando",
            Sobrenome = "Rodriguez",
            Email = "armandorodri@cnn.com",
            Nascimento = new DateTime(1996, 1, 5),
            Genero = Genero.Masculino,
            Curso = new Curso { CursoId = 4, Nome = "Algoritmos", Creditos = 40 },
            Foto = "images/foto4.jpg"
        };
        Aluno aluno5 = new Aluno
        {
            AlunoId = 5,
            Nome = "Pedro",
            Sobrenome = "Santiago",
            Email = "pedrosantia@yahoo.com",
            Nascimento = new DateTime(1998, 4, 9),
            Genero = Genero.Masculino,
            Curso = new Curso { CursoId = 5, Nome = "História da arte", Creditos = 70 },
            Foto = "images/foto5.jpg"
        };

        Alunos = new List<Aluno> { aluno1, aluno2, aluno3, aluno4, aluno5 };
    }
}
