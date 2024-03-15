using k8s.Models;
using KubeOps.Abstractions.Entities;

namespace SmoothOperator;

[KubernetesEntity(Group = "intre.com", ApiVersion = "v1", Kind = "Human")]
public class Human :
    CustomKubernetesEntity<Human.EntitySpec, Human.EntityStatus>
{
    public override string ToString()
        => $"Test Entity ({Metadata.Name}): {Spec.Username} ({Spec.Email})";

    public class EntitySpec
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }

    public class EntityStatus
    {
        public string Status { get; set; } = string.Empty;
    }
}
