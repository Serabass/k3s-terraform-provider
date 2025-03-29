using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TerraformPluginDotNet.ResourceProvider;

namespace K3SProvider;

public class K3SClusterProvider : IResourceProvider<ClusterResource>
{
  private readonly SampleConfigurator _configurator;

  public K3SClusterProvider(SampleConfigurator configurator)
  {
    _configurator = configurator;
  }

  public Task<ClusterResource> PlanAsync(ClusterResource? prior, ClusterResource proposed)
  {
    return Task.FromResult(proposed);
  }

  public Task<ClusterResource> CreateAsync(ClusterResource planned)
  {
    planned.Id = Guid.NewGuid().ToString();
    return Task.FromResult(planned);
  }

  public Task DeleteAsync(ClusterResource resource)
  {
    return Task.CompletedTask;
  }

  public Task<ClusterResource> ReadAsync(ClusterResource resource)
  {
    return Task.FromResult(resource);
  }

  public Task<ClusterResource> UpdateAsync(ClusterResource? prior, ClusterResource planned)
  {
    return Task.FromResult(planned);
  }

  public Task<IList<ClusterResource>> ImportAsync(string name)
  {
    throw new NotImplementedException();
  }
}
