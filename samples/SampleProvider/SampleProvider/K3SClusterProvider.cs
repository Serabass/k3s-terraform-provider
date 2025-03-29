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

    public async Task<K3SClusterResource> CreateAsync(K3SClusterResource planned)
    {
        return planned;
    }

    public Task DeleteAsync(K3SClusterResource resource)
    {
        return Task.CompletedTask;
    }

    public async Task<K3SClusterResource> ReadAsync(K3SClusterResource resource)
    {
        return resource;
    }

    public async Task<K3SClusterResource> UpdateAsync(K3SClusterResource? prior, K3SClusterResource planned)
    {
        return planned;
    }

    public Task<IList<K3SClusterResource>> ImportAsync(string name)
    {
        // Id is not K3SClusterResource.Id, it's the "import ID" supplied by Terraform
        // and in this provider, is defined to be the file name.

        return Task.FromResult<IList<K3SClusterResource>>(
            new[] { new K3SClusterResource { Name = name } });
    }
}
