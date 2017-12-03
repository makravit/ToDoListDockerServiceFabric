using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Api.Models;

namespace ToDoList.Api.Data.Mappings
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.ToTable("TodoItem");
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        }
    }
}
