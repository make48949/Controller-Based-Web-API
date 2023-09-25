using Microsoft.EntityFrameworkCore;

namespace Controller_Based_Web_API.Models
{
    public class ToDoContext: DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) 
        {

            
        }

        public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
    }
}
