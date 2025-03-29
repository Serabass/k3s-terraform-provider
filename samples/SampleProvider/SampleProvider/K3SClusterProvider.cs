using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TerraformPluginDotNet.ResourceProvider;

namespace SampleProvider;

public class K3SClusterProvider : IResourceProvider<K3SClusterResource>
{
    private readonly SampleConfigurator _configurator;

    public K3SClusterProvider(SampleConfigurator configurator)
    {
        _configurator = configurator;
    }

    public Task<K3SClusterResource> PlanAsync(K3SClusterResource? prior, K3SClusterResource proposed)
    {
        return Task.FromResult(proposed);
    }

    public Task<K3SClusterResource> CreateAsync(K3SClusterResource planned)
    {
        return Task.FromResult(planned);
    }

    public Task DeleteAsync(K3SClusterResource resource)
    {
        return Task.CompletedTask;
    }

    public Task<K3SClusterResource> ReadAsync(K3SClusterResource resource)
    {
        return Task.FromResult(resource);
    }

    public Task<K3SClusterResource> UpdateAsync(K3SClusterResource? prior, K3SClusterResource planned)
    {
        return Task.FromResult(planned);
    }

    public Task<IList<K3SClusterResource>> ImportAsync(string name)
    {
        throw new NotImplementedException();
    }
}
