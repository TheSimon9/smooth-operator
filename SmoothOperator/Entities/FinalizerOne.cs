using KubeOps.Abstractions.Finalizer;
using Microsoft.Extensions.Logging;

namespace SmoothOperator;

public class FinalizerOne(ILogger<HumanController> logger) : IEntityFinalizer<HumanEntity>
{
    // Called when entity is marked as deleted
    public Task FinalizeAsync(HumanEntity entity)
    {
        logger.LogInformation("Deleting an amazing human: {Entity}.", entity);
        return Task.CompletedTask;
    }
}