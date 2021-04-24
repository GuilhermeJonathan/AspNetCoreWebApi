using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly SmartContext _context;

        public AlunoController(SmartContext context)
        {
            _context = context;
        }

        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id){
            
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            
            if(aluno == null)
                return BadRequest("Aluno não encontrado.");

            return Ok(aluno);
         }

        [HttpGet("byName/{nome}")]
        public IActionResult GetByNome(string nome){
            
            var aluno = _context.Alunos.FirstOrDefault(a => a.Nome.Contains(nome) || a.Sobrenome.Contains(nome));
            
            if(aluno == null)
                return BadRequest("Aluno não encontrado.");

            return Ok(aluno);
         }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }
         
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            //AsNoTracking permite não bloquear o dado (usuário)
            var alunoBanco = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            
            if(alunoBanco == null)
                return BadRequest("Não encontrou nenhum aluno para ser atualizado.");

            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }
        
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        { 
            //PATCH atualiza somente alguns campos
            var alunoBanco = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            
            if(alunoBanco == null)
                return BadRequest("Não encontrou nenhum aluno para ser atualizado.");

            _context.Update(aluno);
            _context.SaveChanges();    
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            
            if(aluno == null)
                return BadRequest("Não encontrou nenhum aluno para ser deletado.");

            _context.Remove(aluno);
            _context.SaveChanges();
            return Ok();
        }
        
    }
}