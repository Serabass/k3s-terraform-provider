using System.Threading.Tasks;
using K3SProvider.Providers;
using K3SProvider.Resources;
using Microsoft.Extensions.DependencyInjection;
using TerraformPluginDotNet;
using TerraformPluginDotNet.ResourceProvider;

namespace K3SProvider;

class Program
{
  static Task Main(string[] args)
  {
    // Use the default plugin host that takes care of certificates and hosting the Grpc services.

    return TerraformPluginHost.RunAsync(
      args,
      "example.com/serabass/k3s",
      (services, registry) =>
      {
        services.AddSingleton<SampleConfigurator>();
        services.AddTerraformProviderConfigurator<Configuration, SampleConfigurator>();

        services.AddSingleton<IResourceProvider<ClusterResource>, ClusterProvider>();
        services.AddSingleton<IResourceProvider<ServerResource>, ServerProvider>();
        services.AddSingleton<IResourceProvider<AgentResource>, AgentProvider>();
        services.AddSingleton<IResourceProvider<SandboxResource>, SandboxProvider>();

        registry.RegisterResource<ClusterResource>("k3s_cluster");
        registry.RegisterResource<ServerResource>("k3s_server");
        registry.RegisterResource<AgentResource>("k3s_agent");
        registry.RegisterResource<SandboxResource>("k3s_sandbox");
      });
  }
}
