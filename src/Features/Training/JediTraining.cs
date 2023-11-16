// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Mvc;

namespace JediTrainingTracker.Features.Training;

public static class JediTraining
{
    public record BeginJediTraining(string JediId, DateTime StartDatum);

    [AggregateHandler, WolverinePost("/jedi/begintraining")]
    public static JediTrainingBegonnen Handle(BeginJediTraining command, Jedi _)
    {
        return new JediTrainingBegonnen(command.StartDatum);
    }

    public record VoltooiProefVanBekwaamheid(string JediId, string ProefType, bool IsGeslaagd) : IBegonnenMetTraining;

    [AggregateHandler, WolverinePost("/jedi/voltooiProef")]
    public static ProefVanBekwaamheidVoltooid Handle(VoltooiProefVanBekwaamheid command, Jedi _)
    {
        return new ProefVanBekwaamheidVoltooid(command.ProefType, command.IsGeslaagd);
    }

    public record KenJediRidderschapToe(string JediId, DateTime DatumVanToekenning, string ToegekendDoor) : IBegonnenMetTraining;

    [AggregateHandler, WolverinePost("/jedi/ridderschap")]
    public static JediRidderschapToegekend Handle(KenJediRidderschapToe command, Jedi _)
    {
        return new JediRidderschapToegekend(command.DatumVanToekenning, command.ToegekendDoor);
    }
}