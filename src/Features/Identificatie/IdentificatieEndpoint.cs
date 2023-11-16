

// ReSharper disable UnusedMember.Global

namespace JediTrainingTracker.Features.Identificatie;

public record IdentificeerForceGevoeligIndividu(
    string JediId,
    string Naam,
    string Locatie
);

public static class IdentificatieEndpoint
{
    [WolverinePost("/jeditrainee/identificeer")]
    public static IStartStream Handle(IdentificeerForceGevoeligIndividu command)
    {
        return MartenOps.StartStream<Jedi>(command.JediId, new ForceGevoeligIndividuGeïdentificeerd(
            command.JediId,
            command.Naam,
            command.Locatie
        ));
    }
}