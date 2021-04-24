using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;

        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var resultado = _repo.GetAllProfessores(true);
            if(resultado != null)
                return Ok(resultado);

            return BadRequest("Professores não encontrados.");
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id){
            
            var professor = _repo.GetProfessorById(id);
            
            if(professor == null)
                return BadRequest("Professor não encontrado.");

            return Ok(professor);
        }
        
        [HttpPost]
        public IActionResult Post(Professor professor)
        {
           _repo.Add(professor);
            
            if(_repo.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não cadastrado");
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            //AsNoTracking permite não bloquear o dado (usuário)
            var professorBanco = _repo.GetProfessorById(id);
            
            if(professorBanco == null)
                return BadRequest("Não encontrou nenhum professor para ser atualizado.");

            _repo.Update(professorBanco);

             if(_repo.SaveChanges())
                return Ok(professorBanco);

            return BadRequest("Professor não atualizado.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        { 
            //PATCH atualiza somente alguns campos
            var professorBanco = _repo.GetProfessorById(id);
            
            if(professorBanco == null)
                return BadRequest("Não encontrou nenhum professor para ser atualizado.");

            _repo.Update(professor);
            
            if(_repo.SaveChanges())
                return Ok(professor);
         
            return BadRequest("Professor não atualizado.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professorBanco = _repo.GetProfessorById(id);
            
            if(professorBanco == null)
                return BadRequest("Não encontrou nenhum professor para ser deletado.");

            _repo.Delete(professorBanco);
            
            if(_repo.SaveChanges())
                return Ok();
            
            return BadRequest("Professor não deletado.");
        }
    }
}