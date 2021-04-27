using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.v1.Dtos;
using SmartSchool.WebAPI.Models;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;

namespace SmartSchool.WebAPI.v1.Controllers
{
    /// <summary>
    /// /Controller do Aluno - V1
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
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
        public async Task<IActionResult> Get([FromQuery]PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, false);   
            var retorno = _mapper.Map<IEnumerable<AlunoDto>>(alunos);
            
            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

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
        /// Método responsável para cadastrar Aluno
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);

            if (_repo.SaveChanges())
                return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

            return BadRequest("Aluno não cadastrado.");
        }

        /// <summary>
        /// Método para atualizar todos os dados de um Aluno
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Método responsável para atualizar qualquer campo de um Aluno
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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