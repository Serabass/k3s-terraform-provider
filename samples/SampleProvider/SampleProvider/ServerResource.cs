using System.ComponentModel;
using MessagePack;
using TerraformPluginDotNet.Resources;

namespace SampleProvider;

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
    [Description(" Host of the server.")]
    [Required]
    public string Host { get; set; } = null!;

    [Key("port")]
    [Description("Port of the server.")]
    public int Port { get; set; }

    [Key("username")]
    [Description("Username of the server.")]
    public string Username { get; set; } = null!;

    [Key("password")]
    [Description("Password of the server.")]
    public string Password { get; set; } = null!;

    [Key("ssh_key")]
    [Description("SSH key of the server.")]
    public string SshKey { get; set; } = null!;
}
