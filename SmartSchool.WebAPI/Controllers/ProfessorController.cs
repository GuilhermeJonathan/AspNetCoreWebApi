using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;
        
        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var resultado = _repo.GetAllProfessores(false);
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
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

           _repo.Add(professor);
            
            if(_repo.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));

            return BadRequest("Professor não cadastrado");
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            
            var professor = _repo.GetProfessorById(id);
            
            if(professor == null)
                return BadRequest("Não encontrou nenhum professor para ser atualizado.");

            _mapper.Map(model, professor);

            _repo.Update(professor);

             if(_repo.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));

            return BadRequest("Professor não atualizado.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        { 
            //PATCH atualiza somente alguns campos
            var professor = _repo.GetProfessorById(id);
            
            if(professor == null)
                return BadRequest("Não encontrou nenhum professor para ser atualizado.");

            _mapper.Map(model, professor);
            
            _repo.Update(professor);
            
            if(_repo.SaveChanges())
                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
         
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