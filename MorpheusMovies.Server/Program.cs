using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using MorpheusMovies.Server.EF;
using MorpheusMovies.Server.MLModel;
using MorpheusMovies.Server.Utilities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TODO")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mlContext = new MLContext();
var trainingData = ModelTraining.LoadTrainingData(mlContext, builder.Services.BuildServiceProvider());
var model = ModelTraining.TrainModel(mlContext, trainingData);

builder.Services.AddSingleton(model);

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}
