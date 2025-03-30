using System.ComponentModel;
using K3SProvider.K3S;
using MessagePack;
using TerraformPluginDotNet.Resources;
using TerraformPluginDotNet.Serialization;

namespace K3SProvider.Resources;

[SchemaVersion(1)]
[MessagePackObject]
public class SandboxResource
{
  [Key("cluster_id")]
  [Description("ID of the cluster.")]
  [Required]
  required public string ClusterId { get; set; }
}
