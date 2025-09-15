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


        [HttpGet("Genero")]
        public async Task<IActionResult> SelecionarLivrosPorGenero(string genero)
        {
            var resposta = await _service.SelecionarLivrosPorGenero(genero);
            return resposta.Success ? Ok(resposta) : BadRequest(resposta);
        }

        [HttpGet("Autor")]
        public async Task<IActionResult> SelecionarLivrosPorAutor(string autor)
        {
            var resposta = await _service.SelecionarLivrosPorAutor(autor);
            return resposta.Success ? Ok(resposta) : BadRequest(resposta);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarLivro([FromBody] LivrosModel livro)
        {
            var resposta = await _service.CadastrarLivro(livro);
            return resposta.Success ? Ok(resposta) : BadRequest(resposta);
        }

        [HttpDelete("{nomeLivro}")]
        public async Task<IActionResult> DeletarLivroPorNome(string nomeLivro)
        {
            var resposta = await _service.DeletarLivroPorNome(nomeLivro);
            return resposta.Success ? Ok(resposta) : BadRequest(resposta);
        }
    }
}
