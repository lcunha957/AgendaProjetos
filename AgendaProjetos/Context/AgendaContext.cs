using AgendaTarefass.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaTarefass.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}