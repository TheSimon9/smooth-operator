using k8s.Models;
using KubeOps.Abstractions.Finalizer;
using KubeOps.KubernetesClient;
using Microsoft.Extensions.Logging;

namespace SmoothOperator;

public class FinalizerOne(ILogger<HumanController> logger, IKubernetesClient client) : IEntityFinalizer<HumanEntity>
{
    // Called when entity is marked as deleted
    public Task FinalizeAsync(HumanEntity entity)
    {
        logger.LogInformation("Deleting an amazing human: {Entity}.", entity);
        client.Delete<V1Pod>(entity.Metadata.Name, entity.Metadata.NamespaceProperty);
        return Task.CompletedTask;
    }
}