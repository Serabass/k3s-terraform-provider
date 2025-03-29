using System.ComponentModel;
using MessagePack;
using TerraformPluginDotNet.Resources;
using TerraformPluginDotNet.Serialization;

namespace K3SProvider.Resources;

[SchemaVersion(1)]
[MessagePackObject]
public class ClusterResource
{
  [Key("id")]
  [Description("ID of the cluster.")]
  [Computed]
  [MessagePackFormatter(typeof(ComputedStringValueFormatter))]
  public string Id { get; set; } = null!;

  [Key("name")]
  [Description("Name of the cluster.")]
  [Required]
  public string Name { get; set; } = null!;
}
