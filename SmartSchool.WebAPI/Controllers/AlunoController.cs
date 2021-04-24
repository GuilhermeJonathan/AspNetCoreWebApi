using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>(){
            new Aluno(){
                Id = 1,
                Nome = "Guilherme",
                Sobrenome = "Joanthan",
                Telefone  = "619999-8888"
            },
            new Aluno(){
                Id = 3,
                Nome = "Jonathan",
                Sobrenome = "Rodrigues",
                Telefone  = "619558-8541"
            },
            new Aluno(){
                Id = 2,
                Nome = "Silva",
                Sobrenome = "Rodrigues",
                Telefone  = "619999-5654"
            }

        };
        public AlunoController() {}   
        
        
        public IActionResult Get()
        {
            return Ok(Alunos);
         }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id){
            
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            
            if(aluno == null)
                return BadRequest("Aluno não encontrado.");

            return Ok(aluno);
         }

        [HttpGet("byName/{nome}")]
        public IActionResult GetByNome(string nome){
            
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome) || a.Sobrenome.Contains(nome));
            
            if(aluno == null)
                return BadRequest("Aluno não encontrado.");

            return Ok(aluno);
         }

        [HttpPost]
        public IActionResult Post(Aluno aluno){
            
            return Ok(aluno);
         }
         
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno){
            
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno){
            
            return Ok(aluno);
        }
    }
}