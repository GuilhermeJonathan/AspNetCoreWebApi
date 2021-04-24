using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;
        
        public Repository(SmartContext context)
        {
             _context = context;
        }

        public void Add<T>(T item) where T : class
        {
             _context.Add(item);
        }

        public void Update<T>(T item) where T : class
        {
             _context.Update(item);
        }

        public void Delete<T>(T item) where T : class
        {
            _context.Remove(item);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public Aluno[] GetAllAlunos(bool incluirDisciplina = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            
            if(incluirDisciplina)
                query = query.Include(a => a.AlunosDisciplinas)
                .ThenInclude(b => b.Disciplina)
                .ThenInclude(c => c.Professor);

            query = query.AsNoTracking().OrderBy(a => a.Nome);
            return query.ToArray();
        }

        public Aluno[] GetAlunosByDisciplinaId(int disciplinaId, bool incluirDisciplina = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            
            if(incluirDisciplina)
                query = query.Include(a => a.AlunosDisciplinas)
                .ThenInclude(b => b.Disciplina)
                .ThenInclude(c => c.Professor);

            query = query.AsNoTracking()
                .OrderBy(a => a.Nome).Where(b => b.AlunosDisciplinas.Any(c => c.Disciplina.Id == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAllAlunoById(int alunoId, bool incluirDisciplina = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            
            if(incluirDisciplina)
                query = query.Include(a => a.AlunosDisciplinas)
                .ThenInclude(b => b.Disciplina)
                .ThenInclude(c => c.Professor);

            query = query.AsNoTracking()
                .OrderBy(a => a.Nome)
                .Where(b => b.Id == alunoId);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool incluirAlunos = false)
        {
            IQueryable<Professor> query = _context.Professores;
            
            if(incluirAlunos)
                query = query.Include(a => a.Disciplinas)
                .ThenInclude(b => b.AlunosDisciplinas)
                .ThenInclude(c => c.Aluno);

            query = query.AsNoTracking().OrderBy(a => a.Nome);
            return query.ToArray();
        }

        public Professor[] GetProfessoresByDisciplinaId(int disciplinaId, bool incluirAlunos = false)
        {
             IQueryable<Professor> query = _context.Professores;
            
            if(incluirAlunos)
                query = query.Include(a => a.Disciplinas)
                .ThenInclude(b => b.AlunosDisciplinas)
                .ThenInclude(c => c.Aluno);

            query = query.AsNoTracking()
                .OrderBy(a => a.Nome).Where(b => b.Disciplinas.Any(c => c.AlunosDisciplinas.Any( d => d.Disciplina.Id == disciplinaId)));

            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool incluirAlunos = false)
        {
             IQueryable<Professor> query = _context.Professores;
            
            if(incluirAlunos)
                query = query.Include(a => a.Disciplinas)
                .ThenInclude(b => b.AlunosDisciplinas)
                .ThenInclude(c => c.Aluno);

            query = query.AsNoTracking()
                .OrderBy(a => a.Nome)
                .Where(b => b.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}