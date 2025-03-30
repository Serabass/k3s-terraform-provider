using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using K3SProvider.K3S;
using K3SProvider.Resources;
using TerraformPluginDotNet.ResourceProvider;

namespace K3SProvider.Providers;

public class SandboxProvider : IResourceProvider<SandboxResource>
{
  private readonly SampleConfigurator _configurator;

  public SandboxProvider(SampleConfigurator configurator)
  {
    _configurator = configurator;
  }

  public Task<SandboxResource> PlanAsync(SandboxResource? prior, SandboxResource proposed)
  {
    return Task.FromResult(proposed);
  }

  public Task<SandboxResource> CreateAsync(SandboxResource planned)
  {
    return Task.FromResult(planned);
  }

  public Task DeleteAsync(SandboxResource resource)
  {
    return Task.CompletedTask;
  }

  public Task<SandboxResource> ReadAsync(SandboxResource resource)
  {
    return Task.FromResult(resource);
  }

  public Task<SandboxResource> UpdateAsync(SandboxResource? prior, SandboxResource planned)
  {
    return Task.FromResult(planned);
  }

  public Task<IList<SandboxResource>> ImportAsync(string id)
  {
    throw new NotImplementedException();
  }
}
