using Microsoft.ML;
using Microsoft.ML.Trainers;
using MorpheusMovies.Server.EF;
using MorpheusMovies.Server.EF.Model;
using MorpheusMovies.Server.ML.Model;

namespace MorpheusMovies.Server.MLModel;

public static class ApplicationMLModel
{
    public static ITransformer TrainModel(MLContext mlContext, IDataView dataView)
    {
        var dataProcessingPipeline = mlContext.Transforms.Conversion
            .MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: nameof(ApplicationUser.UserId))
            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "movieIdEncoded", inputColumnName: nameof(Movie.MovieId)))
            .Append(mlContext.Transforms.Concatenate("Features",
                nameof(ApplicationUser.Age),
                nameof(ApplicationUser.Gender),
                nameof(ApplicationUser.Occupation),
                nameof(Movie.Unknown),
                nameof(Movie.Action),
                nameof(Movie.Adventure),
                nameof(Movie.Animation),
                nameof(Movie.Childrens),
                nameof(Movie.Comedy),
                nameof(Movie.Crime),
                nameof(Movie.Documentary),
                nameof(Movie.Drama),
                nameof(Movie.Fantasy),
                nameof(Movie.FilmNoir),
                nameof(Movie.Horror),
                nameof(Movie.Musical),
                nameof(Movie.Mystery),
                nameof(Movie.Romance),
                nameof(Movie.SciFi),
                nameof(Movie.Thriller),
                nameof(Movie.War),
                nameof(Movie.Western),
                nameof(Rating.RatingValue)));

        var options = new MatrixFactorizationTrainer.Options
        {
            MatrixColumnIndexColumnName = "userIdEncoded",
            MatrixRowIndexColumnName = "movieIdEncoded",
            LabelColumnName = "Rating",
            NumberOfIterations = 20,
            ApproximationRank = 100
        };

        var trainer = mlContext.Recommendation().Trainers.MatrixFactorization(options);
        var trainingPipeline = dataProcessingPipeline.Append(trainer);
        var model = trainingPipeline.Fit(dataView);

        return model;
    }

    public static IDataView LoadTrainingData(MLContext mlContext, IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var ratingsData = from rating in context.Ratings
                          join user in context.ApplicationUsers on rating.UserId equals user.UserId
                          join movie in context.Movies on rating.MovieId equals movie.MovieId
                          select new
                          {
                              UserId = (float)rating.UserId,
                              MovieId = (float)rating.MovieId,
                              Rating = rating.RatingValue,
                              user.Age,
                              user.Gender,
                              user.Occupation,
                              movie.Unknown,
                              movie.Action,
                              movie.Adventure,
                              movie.Animation,
                              movie.Childrens,
                              movie.Comedy,
                              movie.Crime,
                              movie.Documentary,
                              movie.Drama,
                              movie.Fantasy,
                              movie.FilmNoir,
                              movie.Horror,
                              movie.Musical,
                              movie.Mystery,
                              movie.Romance,
                              movie.SciFi,
                              movie.Thriller,
                              movie.War,
                              movie.Western
                          };
        return mlContext.Data.LoadFromEnumerable(ratingsData);
    }

    public static List<MovieRatingPrediction> Predict(MLContext mlContext, ITransformer model, ApplicationUser user)
    {
        var predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
        var recommendations = new List<MovieRatingPrediction>();

        foreach (var movie in user.MoviePreferences)
        {
            var movieRating = new MovieRating
            {
                UserId = user.UserId,
                MovieId = movie.MovieId,
                Age = user.Age,
                Gender = user.Gender.ToString(),
                Occupation = user.Occupation.ToString()
            };

            var prediction = predictionEngine.Predict(movieRating);

            if (prediction.Score > 3.5)
                recommendations.Add(prediction);
        }

        return recommendations;
    }
}

