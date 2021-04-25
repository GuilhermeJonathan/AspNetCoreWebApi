using System.Collections.Generic;
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
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(false);
            
            var retorno = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            if (retorno != null)
                return Ok(retorno);

            return BadRequest("Alunos não encontrados.");
        }

        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAllAlunoById(id, false);

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            if (alunoDto != null)
                return Ok(alunoDto);

            return BadRequest("Aluno não encontrado.");
        }

        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);

            if (_repo.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não cadastrado.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoRegistrarDto model)
        {
            //AsNoTracking permite não bloquear o dado (usuário)
            var aluno = _repo.GetAllAlunoById(id);

            if (aluno == null)
                return BadRequest("Não encontrou nenhum aluno para ser atualizado.");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);

            if (_repo.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não atualizado.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
        {
            //PATCH atualiza somente alguns campos
            var aluno = _repo.GetAllAlunoById(id);

            if (aluno == null)
                return BadRequest("Não encontrou nenhum aluno para ser atualizado.");

            _mapper.Map(model, aluno);

            _repo.Update(aluno);

            if (_repo.SaveChanges())
                return Ok(aluno);

            return BadRequest("Aluno não atualizado.");

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAllAlunoById(id);

            if (aluno == null)
                return BadRequest("Não encontrou nenhum aluno para ser deletado.");

            _repo.Delete(aluno);

            if (_repo.SaveChanges())
                return Ok();

            return BadRequest("Aluno não deletado.");
        }

    }
}