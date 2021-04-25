using AutoMapper;
using SmartSchool.WebAPI.v2.Dtos;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.v2.Profiles
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
        }
    }
}