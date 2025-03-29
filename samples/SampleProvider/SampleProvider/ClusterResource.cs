using System.ComponentModel;
using MessagePack;
using TerraformPluginDotNet.Resources;

namespace SampleProvider;

[SchemaVersion(1)]
[MessagePackObject]
public class ClusterResource
{
    [Key("name")]
    [Description("Name of the cluster.")]
    [Required]
    public string Name { get; set; } = null!;
}
