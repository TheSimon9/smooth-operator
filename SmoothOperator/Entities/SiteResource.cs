using k8s.Models;
using KubeOps.Abstractions.Entities;

namespace SmoothOperator;

[KubernetesEntity(Group = "intre.com", ApiVersion = "v1", Kind = "Site")]
public class SiteResource :
    CustomKubernetesEntity<SiteResource.SiteSpec, SiteResource.SiteStatus>
{
    public override string ToString()
        => $"An amazing website ({Metadata.Name})!";

    public class SiteSpec
    {
        public string Name { get; set; } = string.Empty;
        
        public string Address { get; set; } = string.Empty;
        
        public string Message { get; set; } = string.Empty;
    }

    public class SiteStatus
    {
        public string Status { get; set; } = string.Empty;
    }
}
