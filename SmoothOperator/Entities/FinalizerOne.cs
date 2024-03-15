using KubeOps.Abstractions.Finalizer;
using Microsoft.Extensions.Logging;

namespace SmoothOperator;

public class FinalizerOne(ILogger<V1TestEntityController> logger) : IEntityFinalizer<Human>
{
    // Called when entity is marked as deleted
    public Task FinalizeAsync(Human entity)
    {
        logger.LogInformation("Deleting an amazing human: {Entity}.", entity);
        return Task.CompletedTask;
    }
}