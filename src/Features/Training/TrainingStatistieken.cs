using Marten.Events.Projections;

namespace JediTrainingTracker.Features.Training;

/// <summary>
/// Het doel van deze aggregatie is om gedetailleerde inzichten te bieden in
/// hoe Jedi-trainees presteren in verschillende soorten Proeven van Bekwaamheid.
/// Dit zou kunnen helpen bij het identificeren van gebieden waar trainees
/// extra ondersteuning of training nodig hebben en
/// bij het aanpassen van de trainingsprogramma's om beter aan hun behoeften te voldoen.
/// </summary>
public class TrainingStatistieken
{
    public string Id { get; set; }
    public int AantalGeslaagd { get; set; }
    public int AantalGefaald { get; set; }
}

public class TrainingStatistiekenProjection : MultiStreamProjection<TrainingStatistieken, string>
{
    public TrainingStatistiekenProjection()
    {
        Identity<ProefVanBekwaamheidVoltooid>(x => x.ProefType);
    }

    public void Apply(ProefVanBekwaamheidVoltooid @event, TrainingStatistieken view)
    {
        if (@event.IsGeslaagd)
            view.AantalGeslaagd++;
        else
            view.AantalGefaald++;
    }
}