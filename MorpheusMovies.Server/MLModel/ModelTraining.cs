using Microsoft.ML;
using MorpheusMovies.Server.Models;

namespace MorpheusMovies.Server.MLModel;

public class ModelTraining
{
    private static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "movie-ratings.csv");

    public static ITransformer TrainModel()
    {
        var context = new MLContext();
        var data = context.Data.LoadFromTextFile<ModelInput>(_dataPath, separatorChar: ',', hasHeader: true);

        var pipeline = context.Transforms.Conversion.MapValueToKey("UserIdEncoded", nameof(ModelInput.UserId))
            .Append(context.Transforms.Conversion.MapValueToKey("MovieIdEncoded", nameof(ModelInput.MovieId)))
            .Append(context.Recommendation().Trainers.MatrixFactorization(
                labelColumnName: "Label",
                matrixColumnIndexColumnName: "UserIdEncoded",
                matrixRowIndexColumnName: "MovieIdEncoded"));

        var model = pipeline.Fit(data);
        return model;
    }

    public static float Predict(ITransformer model, float userId, float movieId)
    {
        var context = new MLContext();
        var predictionEngine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

        var prediction = predictionEngine.Predict(new ModelInput { UserId = userId, MovieId = movieId });
        return prediction.Score;
    }
}
