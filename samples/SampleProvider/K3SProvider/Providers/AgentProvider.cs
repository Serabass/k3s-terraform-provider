﻿using System;
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
    var installer = new K3SInstaller(planned);
    installer.InstallK3SAgent(version, planned.Url, planned.Token);
    return Task.FromResult(planned);
  }

  public Task DeleteAsync(AgentResource resource)
  {
    var installer = resource.CreateInstaller();
    installer.UninstallK3SAgent();

    return Task.CompletedTask;
  }

  public Task<AgentResource> ReadAsync(AgentResource resource)
  {
    var installer = resource.CreateInstaller();
    resource.Token = installer.GetK3SServerToken();

    return Task.FromResult(resource);
  }

  public Task<AgentResource> UpdateAsync(AgentResource? prior, AgentResource planned)
  {
    if (prior is null)
    {
      throw new TerraformResourceProviderException("Prior resource is required.");
    }

    if (prior.Version != planned.Version)
    {
      var installer = planned.CreateInstaller();
      installer.InstallK3SAgent(planned.Version, planned.Url, planned.Token);
    }
    return Task.FromResult(planned);
  }

  public Task<IList<AgentResource>> ImportAsync(string id)
  {
    throw new NotImplementedException();
  }
}
