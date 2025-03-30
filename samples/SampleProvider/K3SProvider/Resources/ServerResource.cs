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

  [Key("host")]
  [Description("Host of the server.")]
  [Required]
  public string Host { get; set; } = null!;

  [Key("port")]
  [Description("SSH Port of the server.")]
  public int Port { get; set; }

  [Key("username")]
  [Description("SSH Username of the server.")]
  [Required]
  public string Username { get; set; } = null!;

  [Key("password")]
  [Description("SSH Password of the server.")]
  public string Password { get; set; } = null!;

  [Key("ssh_key")]
  [Description("SSH key of the server.")]
  public string SshKey { get; set; } = null!;

  [Key("token")]
  [Description("Token of the server.")]
  [Computed]
  [MessagePackFormatter(typeof(ComputedStringValueFormatter))]
  public string Token { get; set; } = null!;

  [Key("url")]
  [Description("URL of the server.")]
  [Computed]
  [MessagePackFormatter(typeof(ComputedStringValueFormatter))]
  public string Url { get; set; } = string.Empty;

  public K3SInstaller CreateInstaller() => new(this);
}
