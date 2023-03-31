using TatBlog.WebApi.Endpoints;
using TatBlog.WebApi.Extensions;
using TatBlog.WebApi.Mapsters;
using TatBlog.WebApi.Validations;

var builder = WebApplication.CreateBuilder(args);
{
    //Add services to the container.
    builder
        .ConfigureCors()
        .ConfigureNlog()
        .ConfigureServices()
        .ConfigureSwaggerOpenApi()
        .ConfigureMapster()
        .ConfigureFluentValidation();
    
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline
    app.SetupRequestPipeline();

    // Configure API endpoints
    app.MapAuthorEndpoints();

    // Configure API endpoints
    app.MapCategoryEndpoints();

    // Configure API endpoints
    app.MapTagEndpoints();

    // Configure API endpoints
    app.MapDashboardEndpoints();

    //// Configure API endpoints
    //app.MapCommentEndpoints();

    // Configure API endpoints
    app.MapPostEndpoints();

    app.Run();
}

app.Run();

