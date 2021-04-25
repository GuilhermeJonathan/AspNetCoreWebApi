using System;

namespace SmartSchool.WebAPI.v2.Dtos
{
    /// <summary>
    /// DTO para registro de aluno
    /// </summary>
    public class AlunoRegistrarDto
    {
        /// <summary>
        /// Identificador do Aluno
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Matrícula do Aluno
        /// </summary>
        public int Matricula { get; set; }
        /// <summary>
        /// Nome do Aluno
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Sobre nome do Aluno
        /// </summary>
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; } 
        public bool Ativo { get; set; } 
    }
}