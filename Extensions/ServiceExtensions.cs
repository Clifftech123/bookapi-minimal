
using System.Reflection;
using bookapi_minimal.AppContext;
using bookapi_minimal.Exceptions;
using bookapi_minimal.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace bookapi_minimal.Services
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
        
            // Adding the database context
            builder.Services.AddDbContext<ApplicationContext>(configure =>
            {
                configure.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
            });


             // Adding the   service 

             builder.Services.AddScoped<IBookService, BookService>();

            // Adding validators from the current assembly
             builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
             builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            // Adding problem details
            builder.Services.AddProblemDetails();
        }
    }
}