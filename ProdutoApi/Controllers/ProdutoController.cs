using ProdutoApi.Context;
using ProdutoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProdutoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutoController(AppDbContext context)
        {
          _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var produto = _context.Produtos.Find(id);
            if(produto == null)
            {
                return NotFound($"Produto de {id} não encontrado");
            }
            return Ok(produto);
        }
        
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var produtos = _context.Produtos.ToList();
            return Ok(produtos);
        }

        [HttpGet("getByName")]
        public IActionResult GetByName(string name)
        {
            var produtos = _context.Produtos.Where( p => p.Nome.Contains(name));
            return Ok(produtos);
        }

        [HttpPost]
        public IActionResult Insert(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                return BadRequest(new {Error = "Nome do produto não encontrado"});
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = produto.Id}, produto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Produto produto)
        {
            var produtoArmazenado = _context.Produtos.Find(id);
            if(produtoArmazenado == null) return NotFound();
            if(string.IsNullOrWhiteSpace(produto.Nome)) 
                return BadRequest("nome do produto não pode ser vazio");
            produtoArmazenado.Nome = produto.Nome;
            produtoArmazenado.Estoque = produto.Estoque;
            produtoArmazenado.Preco = produto.Preco;
            _context.Produtos.Update(produtoArmazenado);
            _context.SaveChanges();
            return Ok(produtoArmazenado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var produto = _context.Produtos.Find(id);
            if(produto == null)
                return NotFound();
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}