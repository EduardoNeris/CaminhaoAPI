using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaminhaoApi.Data;
using CaminhaoAPI.Models;

namespace CaminhaoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaminhaoController : ControllerBase
    {
        private readonly CaminhaoContext _context;

        public CaminhaoController(CaminhaoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém uma lista de todos os caminhões.
        /// </summary>
        /// <returns>Uma lista de caminhões.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Caminhao>>> Get()
        {
            return await _context.Caminhoes.ToListAsync();
        }

        /// <summary>
        /// Obtém um caminhão específico pelo ID.
        /// </summary>
        /// <param name="id">O ID do caminhão a ser recuperado.</param>
        /// <returns>O caminhão solicitado ou NotFound se não existir.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Caminhao>> Get(int id)
        {
            var caminhao = await _context.Caminhoes.FindAsync(id);
            if (caminhao == null)
            {
                return NotFound();
            }
            return caminhao;
        }

        /// <summary>
        /// Cria um novo caminhão.
        /// </summary>
        /// <param name="caminhao">Os dados do caminhão a ser criado.</param>
        /// <returns>Um resultado que representa a criação do caminhão.</returns>
        [HttpPost]
        public async Task<ActionResult<Caminhao>> Post(Caminhao caminhao)
        {
            _context.Caminhoes.Add(caminhao);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = caminhao.Id }, caminhao);
        }

        /// <summary>
        /// Atualiza os dados de um caminhão existente.
        /// </summary>
        /// <param name="id">O ID do caminhão a ser atualizado.</param>
        /// <param name="caminhao">Os dados atualizados do caminhão.</param>
        /// <returns>NoContent se a atualização for bem-sucedida, ou BadRequest se o ID não corresponder.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Caminhao caminhao)
        {
            if (id != caminhao.Id)
            {
                return BadRequest();
            }
            _context.Entry(caminhao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Exclui um caminhão pelo ID.
        /// </summary>
        /// <param name="id">O ID do caminhão a ser excluído.</param>
        /// <returns>NoContent se a exclusão for bem-sucedida, ou NotFound se o caminhão não existir.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var caminhao = await _context.Caminhoes.FindAsync(id);
            if (caminhao == null)
            {
                return NotFound();
            }
            _context.Caminhoes.Remove(caminhao);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
