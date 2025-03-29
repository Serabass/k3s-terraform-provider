using System.ComponentModel;
using MessagePack;

namespace K3SProvider;

[MessagePackObject]
public class Configuration
{
  [Key("name")]
  [Description("Name of the cluster.")]
  public string? Name { get; set; }
}
