using k8s.Models;
using KubeOps.Abstractions.Controller;
using KubeOps.Abstractions.Finalizer;
using KubeOps.Abstractions.Rbac;
using KubeOps.KubernetesClient;
using Microsoft.Extensions.Logging;

namespace SmoothOperator;

[EntityRbac(typeof(SiteResource), Verbs = RbacVerb.All)]
public class SiteController(IKubernetesClient client,
        ILogger<SiteController> logger, 
        EntityFinalizerAttacher<SiteFinalizer, 
        SiteResource> finalizer1)
    : IEntityController<SiteResource>
{
    public async Task ReconcileAsync(SiteResource resource, CancellationToken cancellationToken)
    {
        logger.LogInformation("Reconciling resource {Entity}.", resource);

        resource = await finalizer1(resource);
        
        resource.Status.Status = "Reconciling";
        resource = await client.UpdateStatusAsync(resource);
        
        var pod = new V1Pod
        {
            Metadata = new V1ObjectMeta
            {
                Name = resource.Metadata.Name,
                NamespaceProperty = resource.Metadata.NamespaceProperty
            },
            Spec = new V1PodSpec
            {
                Containers = new List<V1Container>
                {
                    new()
                    {
                        Name = "amazing-website",
                        Image = "nginx",
                        Env = new List<V1EnvVar>()
                        {
                            new()
                            {
                                Name = "NAME",
                                Value = resource.Spec.Name
                            },
                            new()
                            {
                                Name = "ADDRESS",
                                Value = resource.Spec.Address
                            },
                            new()
                            {
                                Name = "Message",
                                Value = resource.Spec.Message 
                            }
                        }
                    }
                }
            }
        };
        await client.CreateAsync(pod);
        
        resource.Status.Status = "Reconciled";
        await client.UpdateStatusAsync(resource);
    }
    
    public Task DeletedAsync(SiteResource entity, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}