using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using K3SProvider.K3S;
using K3SProvider.Resources;
using TerraformPluginDotNet.ResourceProvider;

namespace K3SProvider.Providers;

public class AgentProvider : IResourceProvider<AgentResource>
{
  private readonly SampleConfigurator _configurator;

  public AgentProvider(SampleConfigurator configurator)
  {
    _configurator = configurator;
  }

  public Task<AgentResource> PlanAsync(AgentResource? prior, AgentResource proposed)
  {
    return Task.FromResult(proposed);
  }

  public Task<AgentResource> CreateAsync(AgentResource planned)
  {
    var version = _configurator.Config?.K3SVersion;
    var installer = planned.CreateInstaller(version);
    installer.InstallK3SAgent(planned.Url, planned.Token);
    return Task.FromResult(planned);
  }

  public Task DeleteAsync(AgentResource resource)
  {
    var version = _configurator.Config?.K3SVersion;
    var installer = resource.CreateInstaller(version);
    installer.UninstallK3SAgent();

    return Task.CompletedTask;
  }

  public Task<AgentResource> ReadAsync(AgentResource resource)
  {
    var version = _configurator.Config?.K3SVersion;
    var installer = resource.CreateInstaller(version);
    resource.Token = installer.GetK3SServerToken();

    return Task.FromResult(resource);
  }

  public Task<AgentResource> UpdateAsync(AgentResource? prior, AgentResource planned)
  {
    if (prior is null)
    {
      throw new TerraformResourceProviderException("Prior resource is required.");
    }

    return Task.FromResult(planned);
  }

  public Task<IList<AgentResource>> ImportAsync(string id)
  {
    throw new NotImplementedException();
  }
}
