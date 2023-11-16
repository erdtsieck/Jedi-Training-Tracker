using JediTrainingTracker;
using JediTrainingTracker.Features.Training;
using Marten.Events;
using Marten.Events.Projections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Marten")!);
        opts.DatabaseSchemaName = opts.Events.DatabaseSchemaName = "jeditraining";
        opts.Events.StreamIdentity = StreamIdentity.AsString;
        opts.Events
            .AddEventType<ForceGevoeligIndividuGeïdentificeerd>()
            .AddEventType<JediTrainingBegonnen>()
            .AddEventType<ProefVanBekwaamheidVoltooid>()
            .AddEventType<JediRidderschapToegekend>();

        opts.Projections.LiveStreamAggregation<Jedi>();
        opts.Projections.Add<TrainingStatistiekenProjection>(ProjectionLifecycle.Inline);
    })
    .OptimizeArtifactWorkflow()
    .IntegrateWithWolverine();

builder.Host.UseWolverine(opts =>
{
    opts.Policies.AutoApplyTransactions();
    opts.Policies.UseDurableLocalQueues();
    opts.OptimizeArtifactWorkflow();
}).ApplyOaktonExtensions();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger().UseSwaggerUI().UseHttpsRedirection();

app.MapWolverineEndpoints(opts =>
{
    opts.AddMiddlewareByMessageType(typeof(BegonnenMetTraining));
});

return await app.RunOaktonCommands(args);