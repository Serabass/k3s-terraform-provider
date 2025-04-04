using System.ComponentModel;
using K3SProvider.K3S;
using MessagePack;
using TerraformPluginDotNet.Resources;

namespace K3SProvider.Resources;

[SchemaVersion(1)]
[MessagePackObject]
public class AgentResource
{
  [Key("name")]
  [Description("Name of the agent.")]
  [Required]
  public string Name { get; set; } = null!;

  [Key("ssh")]
  [Description("SSH configuration.")]
  [Required]
  public SSH Ssh { get; set; } = null!;

  [Key("url")]
  [Description("URL of the server.")]
  [Required]
  public string Url { get; set; } = null!;

  [Key("token")]
  [Description("Token of the server.")]
  [Required]
  public string Token { get; set; } = null!;

  public K3SInstaller CreateInstaller(string? version) => new(this, version);
}
