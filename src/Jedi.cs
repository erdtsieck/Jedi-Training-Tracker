// ReSharper disable UnusedMember.Global
namespace JediTrainingTracker;

public record ForceGevoeligIndividuGeïdentificeerd(string JediId, string Naam, string Locatie);

public record JediTrainingBegonnen(DateTime StartDatum);

public record ProefVanBekwaamheidVoltooid(string ProefType, bool IsGeslaagd);

public record JediRidderschapToegekend(DateTime DatumVanToekenning, string ToegekendDoor);

public enum JediTraineeStatus
{
    Geïdentificeerd = 1,
    TrainingBegonnen = 2,
    ProefVoltooid = 3,
    RidderschapToegekend = 4
}

public record Jedi(string Id, string Naam, string Locatie)
{
    public DateTime StartDatum { get; private init; }
    public string? ProefType { get; private init; }
    public bool IsGeslaagd { get; private init; }
    public DateTime? DatumVanToekenning { get; private init; }
    public string? ToegekendDoor { get; private init; }
    public JediTraineeStatus Status { get; private init; } = JediTraineeStatus.Geïdentificeerd;

    public static Jedi Create(ForceGevoeligIndividuGeïdentificeerd @event) => 
        new(@event.JediId, @event.Naam, @event.Locatie);

    public Jedi Apply(JediTrainingBegonnen @event) => this with
    {
        StartDatum = @event.StartDatum,
        Status = JediTraineeStatus.TrainingBegonnen
    };

    public Jedi Apply(ProefVanBekwaamheidVoltooid @event) => this with
    {
        ProefType = @event.ProefType,
        IsGeslaagd = @event.IsGeslaagd,
        Status = @event.IsGeslaagd ? JediTraineeStatus.ProefVoltooid : Status
    };

    public Jedi Apply(JediRidderschapToegekend @event) => this with
    {
        DatumVanToekenning = @event.DatumVanToekenning,
        ToegekendDoor = @event.ToegekendDoor,
        Status = JediTraineeStatus.RidderschapToegekend
    };
}
