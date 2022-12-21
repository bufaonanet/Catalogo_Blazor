namespace Alunos.Model
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public DateTime Nascimento { get; set; }
        public Genero Genero { get; set; }
        public Curso Curso { get; set; }
        public string Foto { get; set; }
    }
}