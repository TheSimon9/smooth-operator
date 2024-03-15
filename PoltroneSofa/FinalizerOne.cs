using KubeOps.Abstractions.Finalizer;
using Microsoft.Extensions.Logging;

namespace PoltroneSofa;

public class FinalizerOne(ILogger<V1TestEntityController> logger) : IEntityFinalizer<V1TestEntity>
{
    // Called when entity is marked as deleted
    public Task FinalizeAsync(V1TestEntity entity)
    {
        logger.LogInformation("Deleting entity {Entity}.", entity);
        return Task.CompletedTask;
    }
}