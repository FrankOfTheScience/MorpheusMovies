using Microsoft.ML;
using Microsoft.ML.Trainers;
using MorpheusMovies.Server.EF;
using MorpheusMovies.Server.ML.Model;

namespace MorpheusMovies.Server.MLModel;

public static class ModelTraining
{
    public static ITransformer TrainModel(MLContext mlContext, IDataView trainingData)
    {
        var dataProcessingPipeline = mlContext.Transforms.Conversion
            .MapValueToKey(outputColumnName: "userIdEncoded", inputColumnName: nameof(MovieRating.UserId))
            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "movieIdEncoded", inputColumnName: nameof(MovieRating.MovieId)));

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

        return trainingPipeline.Fit(trainingData);
    }

    public static IDataView LoadTrainingData(MLContext mlContext, IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var ratings = context.Ratings
            .Select(r => new MovieRating { UserId = r.UserId, MovieId = r.MovieId, Rating = r.RatingValue })
            .ToList();
        return mlContext.Data.LoadFromEnumerable(ratings);
    }


    public static List<int> Predict(MLContext mlContext, ITransformer model, float userId, List<int> movieIds)
    {
        var predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
        var recommendations = new List<int>();

        foreach (var movieId in movieIds)
        {
            var prediction = predictionEngine
                .Predict(new MovieRating
                {
                    UserId = userId,
                    MovieId = movieId
                });

            if (prediction.Score > 3.5)
                recommendations.Add(movieId);
        }
        return recommendations;
    }
}

