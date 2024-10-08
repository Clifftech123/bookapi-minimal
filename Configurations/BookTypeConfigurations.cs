
using bookapi_minimal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bookapi_minimal.Configurations
{
    public class BookTypeConfigurations : IEntityTypeConfiguration<BookModel>
    {
        public void Configure(EntityTypeBuilder<BookModel> builder)
        {
            // Configure the table name
            builder.ToTable("Books");

            // Configure the primary key
            builder.HasKey(x => x.Id);

            // Configure properties
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Author).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Category).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Language).IsRequired().HasMaxLength(50);
            builder.Property(x => x.TotalPages).IsRequired();

            // Seed data
            builder.HasData(
                new BookModel
                {
                    Id = Guid.NewGuid(),
                    Title = "The Alchemist",
                    Author = "Paulo Coelho",
                    Description = "The Alchemist follows the journey of an Andalusian shepherd",
                    Category = "Fiction",
                    Language = "English",
                    TotalPages = 208
                },
                new BookModel
                {
                    Id = Guid.NewGuid(),
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Description = "A novel about the serious issues of rape and racial inequality.",
                    Category = "Fiction",
                    Language = "English",
                    TotalPages = 281
                },
                new BookModel
                {
                    Id = Guid.NewGuid(),
                    Title = "1984",
                    Author = "George Orwell",
                    Description = "A dystopian social science fiction novel and cautionary tale about the dangers of totalitarianism. ",
                  Category = "Fiction",
                  Language = "English",
                  TotalPages = 328
                } 
            );
        }
    }
}