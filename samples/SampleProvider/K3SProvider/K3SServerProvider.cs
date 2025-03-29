using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using K3SProvider.K3S;
using TerraformPluginDotNet.ResourceProvider;

namespace K3SProvider;

public class K3SServerProvider : IResourceProvider<ServerResource>
{
  private readonly SampleConfigurator _configurator;

  public K3SServerProvider(SampleConfigurator configurator)
  {
    _configurator = configurator;
  }

  public Task<ServerResource> PlanAsync(ServerResource? prior, ServerResource proposed)
  {
    if (prior is null)
      throw new TerraformResourceProviderException("Prior resource is required.");

    if (proposed.Host != prior.Host)
    {
      prior.CreateInstaller().UninstallK3SServer();

      // proposed.CreateInstaller().InstallK3SServer(proposed.Version);
    }

    return Task.FromResult(proposed);
  }

  public Task<ServerResource> CreateAsync(ServerResource planned)
  {
    var installer = planned.CreateInstaller();
    installer.InstallK3SServer(planned.Version);
    planned.Token = installer.GetK3SServerToken();
    planned.Url = $"https://{planned.Host}:6443";

    return Task.FromResult(planned);
  }

  public Task DeleteAsync(ServerResource resource)
  {
    var installer = resource.CreateInstaller();
    installer.UninstallK3SServer();
    return Task.CompletedTask;
  }

  public Task<ServerResource> ReadAsync(ServerResource resource)
  {
    var installer = resource.CreateInstaller();
    resource.Token = installer.GetK3SServerToken();
    return Task.FromResult(resource);
  }

  public Task<ServerResource> UpdateAsync(ServerResource? prior, ServerResource planned)
  {
    var installer = planned.CreateInstaller();
    installer.InstallK3SServer(planned.Version);
    return Task.FromResult(planned);
  }

  public Task<IList<ServerResource>> ImportAsync(string id)
  {
    throw new NotImplementedException();
  }
}
