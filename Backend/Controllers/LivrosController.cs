using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivrosService _service;

        public LivrosController(ILivrosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> SelecionarLivros()
        {
            var resposta = await _service.SelecionarLivros();
            return resposta.Success ? Ok(resposta) : BadRequest(resposta);
        }

        [HttpGet("{termo}")]
        public async Task<IActionResult> SelecionarLivrosPorTermo(string termo)
        {
            var resposta = await _service.SelecionarLivrosPorTermo(termo);
            return resposta.Success ? Ok(resposta) : BadRequest(resposta);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarLivro([FromBody] LivrosModel livro)
        {
            var resposta = await _service.CadastrarLivro(livro);
            return resposta.Success ? Ok(resposta) : BadRequest(resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarLivro(Guid id)
        {
            var resposta = await _service.DeletarLivro(id);
            return resposta.Success ? Ok(resposta) : BadRequest(resposta);
        }
    }
}
