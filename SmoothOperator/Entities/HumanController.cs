using k8s.Models;
using KubeOps.Abstractions.Controller;
using KubeOps.Abstractions.Finalizer;
using KubeOps.Abstractions.Rbac;
using KubeOps.KubernetesClient;
using Microsoft.Extensions.Logging;

namespace SmoothOperator;

[EntityRbac(typeof(HumanEntity), Verbs = RbacVerb.All)]
public class HumanController(IKubernetesClient client,
        ILogger<HumanController> logger, 
        EntityFinalizerAttacher<FinalizerOne, 
        HumanEntity> finalizer1)
    : IEntityController<HumanEntity>
{
    public async Task ReconcileAsync(HumanEntity entity)
    {
        logger.LogInformation("Reconciling entity {Entity}.", entity);

        entity = await finalizer1(entity);
        
        entity.Status.Status = "Reconciling";
        entity = await client.UpdateStatusAsync(entity);
        
        var pod = new V1Pod
        {
            Metadata = new V1ObjectMeta
            {
                Name = entity.Metadata.Name,
                NamespaceProperty = entity.Metadata.NamespaceProperty
            },
            Spec = new V1PodSpec
            {
                Containers = new List<V1Container>
                {
                    new V1Container
                    {
                        Name = entity.Metadata.Name,
                        Image = "nginx"
                    }
                }
            }
        };
        
        await client.CreateAsync(pod);
        
        entity.Status.Status = "Reconciled";
        await client.UpdateStatusAsync(entity);
    }
}