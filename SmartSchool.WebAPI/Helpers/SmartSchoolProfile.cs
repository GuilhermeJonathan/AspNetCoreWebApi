using AutoMapper;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    d => d.Nome, 
                    opt => opt.MapFrom(src => $"{src.Nome} { src.Sobrenome}")
                ).ForMember(
                    d => d.Idade, 
                    opt => opt.MapFrom(src => src.DataNascimento.PegarIdade())
                );

            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

            CreateMap<Professor, ProfessorDto>()
                .ForMember(
                    d => d.Nome, 
                    opt => opt.MapFrom(src => $"{src.Nome} { src.Sobrenome}")
                );

            CreateMap<ProfessorDto, Professor>();
            CreateMap<Professor, ProfessorRegistrarDto>().ReverseMap();
        }
    }
}