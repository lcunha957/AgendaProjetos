using AgendaTarefass.Models;
using Microsoft.AspNetCore.Mvc;
using AgendaTarefass.Context;

namespace AgendaTarefass.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly AgendaContext _context;

        public TarefaController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            _context.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefas = _context.Tarefas.ToList();

            return Ok(tarefas);
        }

        [HttpGet("ObterTitulo")]
        public IActionResult ObterTitulo(string titulo)
        {
            var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            return Ok(tarefas);
        }

        [HttpGet("ObterData")]
        public IActionResult ObterData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterStatus")]
        public IActionResult ObterStatus(StatusDaTarefa status)
        {
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaVazio = _context.Tarefas.Find(id);

            if (tarefaVazio == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            tarefaVazio.Data = tarefa.Data;
            tarefaVazio.Descricao = tarefa.Descricao;
            tarefaVazio.Id = tarefa.Id;
            tarefaVazio.Status = tarefa.Status;
            tarefaVazio.Titulo = tarefa.Titulo;

            _context.Update(tarefaVazio);
            _context.SaveChanges();

            return Ok(tarefaVazio);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaVazio = _context.Tarefas.Find(id);

            if (tarefaVazio == null)
                return NotFound();

            _context.Tarefas.Remove(tarefaVazio);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
