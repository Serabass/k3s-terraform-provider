using System.ComponentModel;
using K3SProvider.K3S;
using MessagePack;
using TerraformPluginDotNet.Resources;

namespace K3SProvider.Resources;

[SchemaVersion(2)]
[MessagePackObject]
public class AgentResource
{
  [Key("cluster_id")]
  [Description("ID of the cluster.")]
  [Required]
  public string ClusterId { get; set; } = null!;

  [Key("name")]
  [Description("Name of the agent.")]
  [Required]
  public string Name { get; set; } = null!;

  [Key("host")]
  [Description("Host of the agent.")]
  [Required]
  public string Host { get; set; } = null!;

  [Key("port")]
  [Description("Port of the agent.")]
  public int Port { get; set; }

  [Key("username")]
  [Description("SSH Username of the agent.")]
  [Required]
  public string Username { get; set; } = null!;

  [Key("password")]
  [Description("SSH Password of the agent.")]
  public string Password { get; set; } = null!;

  [Key("ssh_key")]
  [Description("SSH key of the agent.")]
  public string SshKey { get; set; } = null!;

  [Key("url")]
  [Description("URL of the server.")]
  public string Url { get; set; } = null!;

  [Key("token")]
  [Description("Token of the server.")]
  public string Token { get; set; } = null!;

  public K3SInstaller CreateInstaller(string? version) => new(this, version);
}
