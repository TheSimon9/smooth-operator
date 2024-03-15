using k8s.Models;
using KubeOps.Abstractions.Entities;

namespace SmoothOperator;

[KubernetesEntity(Group = "intre.com", ApiVersion = "v1", Kind = "Human")]
public class HumanEntity :
    CustomKubernetesEntity<HumanEntity.EntitySpec, HumanEntity.EntityStatus>
{
    public override string ToString()
        => $"An amazing human being ({Metadata.Name}): {Spec.Username} ({Spec.Name}) with a satisfaction level of {Spec.Satisfaction}!";

    public class EntitySpec
    {
        public string Username { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        
        public int Satisfaction { get; set; } = 0;
    }

    public class EntityStatus
    {
        public string Status { get; set; } = string.Empty;
    }
}
