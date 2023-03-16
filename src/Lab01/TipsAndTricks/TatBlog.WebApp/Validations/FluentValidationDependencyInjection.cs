using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace TatBlog.WebApp.Validations;

public static class FluentValidationDependencyInjection
{
    public static WebApplicationBuilder ConfigureFluentValidation(
        this WebApplicationBuilder buider)
    {
        // Enable client-side integration
        buider.Services.AddFluentValidationClientsideAdapters();

        // Scan and register all validators in given assembly
        buider.Services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly());

        return buider;
    }
}
