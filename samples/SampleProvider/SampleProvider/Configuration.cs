using System.ComponentModel;
using MessagePack;

namespace SampleProvider;

[MessagePackObject]
public class Configuration
{
    [Key("name")]
    [Description("Name of the cluster.")]
    public string? Name { get; set; }
}
