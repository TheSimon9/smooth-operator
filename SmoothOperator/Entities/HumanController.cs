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
        EntityFinalizerAttacher<HumanFinalizer, 
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
                    new()
                    {
                        Name = $"human-being-{entity.Spec.Username}",
                        Image = "nginx",
                        Env = new List<V1EnvVar>()
                        {
                            new()
                            {
                                Name = "USERNAME",
                                Value = entity.Spec.Username
                            },
                            new()
                            {
                                Name = "NAME",
                                Value = entity.Spec.Name
                            },
                            new()
                            {
                                Name = "SATISFACTION",
                                Value = entity.Spec.Satisfaction.ToString()
                            }
                        }
                    }
                }
            }
        };
        await client.CreateAsync(pod);
        
        entity.Status.Status = "Reconciled";
        await client.UpdateStatusAsync(entity);
    }
}