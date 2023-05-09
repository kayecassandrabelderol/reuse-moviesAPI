using Microsoft.OpenApi.Models;
using ArtistApi.Context;
using ArtistApi.Contracts;
using ArtistApi.Repositories;
using ArtistApi.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Artists API",
        Description = "API for Artists and its related attributes: Genres, Songs, and Awards",
        Contact = new OpenApiContact
        {
            Name = "OPPAsara mi Sir",
            Url = new Uri("https://github.com/CITUCCS/csit341-final-project-oppasara-mi-sir")
        }
    });
    //xml documentation
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Configure services
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    //DI
    //DapperContext
    services.AddTransient<DapperContext>();

    //AutoMapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    //Services
    services.AddScoped<ISongService, SongService>();
    services.AddScoped<IGenreService, GenreService>();
    services.AddScoped<IArtistService, ArtistService>();
    services.AddScoped<IAwardService, AwardService>();
    services.AddScoped<ISongArtistService, SongArtistService>();
    services.AddScoped<ISongGenreService, SongGenreService>();

    //Repos
    services.AddScoped<ISongRepository, SongRepository>();
    services.AddScoped<IGenreRepository, GenreRepository>();
    services.AddScoped<IArtistRepository, ArtistRepository>();
    services.AddScoped<IAwardRepository, AwardRepository>();
}
