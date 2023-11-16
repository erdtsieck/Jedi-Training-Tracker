using Microsoft.AspNetCore.Mvc;
using static JediTrainingTracker.Features.Training.JediTraining;

namespace JediTrainingTracker.Features.Training;

public interface IBegonnenMetTraining
{
    string JediId { init; }
}

public class BegonnenMetTraining
{
    [AggregateHandler]
    public static ProblemDetails Before(IBegonnenMetTraining _, Jedi jediTrainee)
    {
        if (jediTrainee.Status != JediTraineeStatus.Geïdentificeerd)
            return new ProblemDetails
            {
                Detail = "Kan geen training beginnen voor iemand die geen Jedi is!!!",
                Status = 400
            };

        return WolverineContinue.NoProblems;
    }
}