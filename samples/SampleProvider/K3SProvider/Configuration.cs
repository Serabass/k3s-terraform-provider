using System.ComponentModel;
using MessagePack;

namespace K3SProvider;

[MessagePackObject]
public class Configuration
{
  [Key("k3s_version")]
  [Description("Version of K3S.")]
  public string? K3SVersion { get; set; }
}
