using System.ComponentModel;
using MessagePack;
using TerraformPluginDotNet.Resources;
using TerraformPluginDotNet.Serialization;

namespace SampleProvider;

[SchemaVersion(1)]
[MessagePackObject]
public class K3SClusterResource
{
    [Key("name")]
    [Description("Name of the cluster.")]
    [Required]
    public string Name { get; set; } = null!;
}
