using System;

namespace SmartSchool.WebAPI.Dtos
{
    public class ProfessorRegistrarDto
    {
        
        public int Id { get; set; }
        public int Registro { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; } 
        public bool Ativo { get; set; }
    }
}