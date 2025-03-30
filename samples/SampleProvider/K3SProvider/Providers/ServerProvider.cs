using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using K3SProvider.K3S;
using K3SProvider.Resources;
using TerraformPluginDotNet.ResourceProvider;

namespace K3SProvider.Providers;

public class ServerProvider : IResourceProvider<ServerResource>
{
  private readonly SampleConfigurator _configurator;

  public ServerProvider(SampleConfigurator configurator)
  {
    _configurator = configurator;
  }

  public Task<ServerResource> PlanAsync(ServerResource? prior, ServerResource proposed)
  {
    if (prior is null)
      return Task.FromResult(proposed);

    if (proposed.Ssh.Host != prior.Ssh.Host)
    {
      var version = _configurator.Config?.K3SVersion;
      var installer = prior.CreateInstaller(version);
      installer.UninstallK3SServer();

      // proposed.CreateInstaller().InstallK3SServer(proposed.Version);
    }

    return Task.FromResult(proposed);
  }

  public Task<ServerResource> CreateAsync(ServerResource planned)
  {
    var version = _configurator.Config?.K3SVersion;
    var installer = planned.CreateInstaller(version);
    installer.InstallK3SServer();
    planned.Token = installer.GetK3SServerToken();
    planned.Url = $"https://{planned.Ssh.Host}:6443";

    return Task.FromResult(planned);
  }

  public Task DeleteAsync(ServerResource resource)
  {
    var version = _configurator.Config?.K3SVersion;
    var installer = resource.CreateInstaller(version);
    installer.UninstallK3SServer();

    return Task.CompletedTask;
  }

  public Task<ServerResource> ReadAsync(ServerResource resource)
  {
    var version = _configurator.Config?.K3SVersion;
    var installer = resource.CreateInstaller(version);
    resource.Token = installer.GetK3SServerToken();
    return Task.FromResult(resource);
  }

  public Task<ServerResource> UpdateAsync(ServerResource? prior, ServerResource planned)
  {
    var version = _configurator.Config?.K3SVersion;
    var installer = planned.CreateInstaller(version);

    // installer.UninstallK3SServer();
    installer.InstallK3SServer();
    return Task.FromResult(planned);
  }

  public Task<IList<ServerResource>> ImportAsync(string id)
  {
    throw new NotImplementedException();
  }
}
