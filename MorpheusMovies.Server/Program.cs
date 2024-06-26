using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using MorpheusMovies.Server.EF;
using MorpheusMovies.Server.MLModel;
using MorpheusMovies.Server.Repository;
using MorpheusMovies.Server.Repository.Interfaces;
using MorpheusMovies.Server.Services;
using MorpheusMovies.Server.Services.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TODO")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mlContext = new MLContext();
var trainingData = ApplicationMLModel.LoadTrainingData(mlContext, builder.Services.BuildServiceProvider());
var model = ApplicationMLModel.TrainModel(mlContext, trainingData);

builder.Services.AddSingleton(model);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IReccomendationService, ReccomendationService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();


