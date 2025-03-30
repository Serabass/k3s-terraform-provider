using System.ComponentModel;
using K3SProvider.K3S;
using MessagePack;
using TerraformPluginDotNet.Resources;
using TerraformPluginDotNet.Serialization;

namespace K3SProvider.Resources;

[SchemaVersion(1)]
[MessagePackObject]
public class ServerResource
{
  [Key("cluster_id")]
  [Description("ID of the cluster.")]
  [Required]
  public string ClusterId { get; set; } = null!;

  [Key("name")]
  [Description("Name of the server.")]
  [Required]
  public string Name { get; set; } = null!;

  [Key("ssh")]
  [Description("SSH configuration.")]
  [Required]
  public SSH Ssh { get; set; } = null!;

  [Key("token")]
  [Description("Token of the server.")]
  [Computed]

  // [MessagePackFormatter(typeof(ComputedStringValueFormatter))]
  public string Token { get; set; } = null!;

  [Key("url")]
  [Description("URL of the server.")]
  [Computed]

  // [MessagePackFormatter(typeof(ComputedStringValueFormatter))]
  public string Url { get; set; } = string.Empty;

  public K3SInstaller CreateInstaller(string? version) => new(this, version);
}
