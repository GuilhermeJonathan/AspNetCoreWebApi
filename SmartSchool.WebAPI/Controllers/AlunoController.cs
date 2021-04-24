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
        public readonly IRepository _repo;

        public AlunoController(IRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Get()
        {
            var resultado = _repo.GetAllAlunos(true);
            
            if(resultado != null)
                return Ok(resultado);
            
            return BadRequest("Alunos não encontrados.");
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAllAlunoById(id, false);

            if (aluno == null)
                return Ok(aluno);

            return BadRequest("Aluno não encontrado.");            
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            _repo.Add(aluno);
            
            if(_repo.SaveChanges())
                return Ok(aluno);
            
            return BadRequest("Aluno não cadastrado.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            //AsNoTracking permite não bloquear o dado (usuário)
            var alunoBanco = _repo.GetAllAlunoById(id);

            if (alunoBanco == null)
                return BadRequest("Não encontrou nenhum aluno para ser atualizado.");

            _repo.Update(aluno);
            
            if(_repo.SaveChanges())
                return Ok(aluno);
            
            return BadRequest("Aluno não atualizado.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            //PATCH atualiza somente alguns campos
            var alunoBanco = _repo.GetAllAlunoById(id);

            if (alunoBanco == null)
                return BadRequest("Não encontrou nenhum aluno para ser atualizado.");

            _repo.Update(aluno);
            
            if(_repo.SaveChanges())
                return Ok(aluno);

            return BadRequest("Aluno não atualizado.");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           var alunoBanco = _repo.GetAllAlunoById(id);

            if (alunoBanco == null)
                return BadRequest("Não encontrou nenhum aluno para ser deletado.");

            _repo.Delete(alunoBanco);
           
            if(_repo.SaveChanges())
                return Ok();

            return BadRequest("Aluno não deletado.");
        }

    }
}