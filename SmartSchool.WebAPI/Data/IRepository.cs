using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T item) where T : class;
        void Update<T>(T item) where T : class;
        void Delete<T>(T item) where T : class;
        bool SaveChanges();

        Aluno[] GetAllAlunos(bool incluirDisciplina = false);
        Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool incluirDisciplina = false);
        Aluno[] GetAlunosByDisciplinaId(int disciplinaId, bool incluirDisciplina = false);
        Aluno GetAllAlunoById(int alunoId, bool incluirDisciplina = false);

        Professor[] GetAllProfessores(bool incluirAlunos = false);
        Professor[] GetProfessoresByDisciplinaId(int disciplinaId, bool incluirAlunos = false);
        Professor GetProfessorById(int professorId, bool incluirAlunos = false);
    }
}