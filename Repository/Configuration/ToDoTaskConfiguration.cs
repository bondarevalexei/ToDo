using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ToDoTaskConfiguration : IEntityTypeConfiguration<ToDoTask>
{
    public void Configure(EntityTypeBuilder<ToDoTask> builder)
    {
        builder.HasData
        (
            new ToDoTask
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                Title = "Title",
                Description = "Description",
                AccountId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new ToDoTask
            {
                Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                Title = "Title1",
                Description = "Description1",
                AccountId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new ToDoTask
            {
                Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                Title = "Title1123",
                Description = "Description11231",
                AccountId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
            }
        );
    }
}
