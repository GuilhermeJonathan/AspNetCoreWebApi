using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.v2.Dtos;

namespace SmartSchool.WebAPI.v2.Controllers
{
    /// <summary>
    /// /Controller do Aluno - V2
    /// </summary>
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;
        /// <summary>
        /// Construtor do Aluno
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        /// <summary>
        /// /Método Responsável para retornar todos os alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(false);
            
            var retorno = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            if (retorno != null)
                return Ok(retorno);

            return BadRequest("Alunos não encontrados.");
        }
        /// <summary>
        /// Método responsável por realizar busca de um aluno por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("byId/{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAllAlunoById(id, false);

            var alunoDto = _mapper.Map<AlunoDto>(aluno);

            if (alunoDto != null)
                return Ok(alunoDto);

            return BadRequest("Aluno não encontrado.");
        }

        /// <summary>
        /// Método responsável por excluir um determinado Aluno
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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