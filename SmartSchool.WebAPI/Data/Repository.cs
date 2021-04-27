using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Helpers;
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

        public  Aluno[] GetAllAlunos(bool incluirDisciplina = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            
            if(incluirDisciplina)
                query = query.Include(a => a.AlunosDisciplinas)
                .ThenInclude(b => b.Disciplina)
                .ThenInclude(c => c.Professor);

            query = query.AsNoTracking().OrderBy(a => a.Nome);
            return query.ToArray();
        }

        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool incluirDisciplina = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            
            if(incluirDisciplina)
                query = query.Include(a => a.AlunosDisciplinas)
                .ThenInclude(b => b.Disciplina)
                .ThenInclude(c => c.Professor);

            query = query.AsNoTracking().OrderBy(a => a.Nome);
            
            if(!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where
                    (a =>   a.Nome.ToUpper().Contains(pageParams.Nome.ToUpper()) ||
                            a.Sobrenome.ToUpper().Contains(pageParams.Nome.ToUpper()) );

            if(pageParams.Matricula > 0)
                query = query.Where(a => a.Matricula == pageParams.Matricula);
            
            if(pageParams.Ativo != null)
                query = query.Where(a => a.Ativo == (pageParams.Ativo == 1 ? true : false) );

            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
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