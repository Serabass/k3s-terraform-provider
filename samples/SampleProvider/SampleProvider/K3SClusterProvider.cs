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
        planned.Id = Guid.NewGuid().ToString();
        await File.WriteAllTextAsync(planned.Path, BuildContent(planned.Content));
        return planned;
    }

    public Task DeleteAsync(K3SClusterResource resource)
    {
        File.Delete(resource.Path);
        return Task.CompletedTask;
    }

    public async Task<K3SClusterResource> ReadAsync(K3SClusterResource resource)
    {
        var content = await File.ReadAllTextAsync(resource.Path);
        resource.Content = content;
        return resource;
    }

    public async Task<K3SClusterResource> UpdateAsync(K3SClusterResource? prior, K3SClusterResource planned)
    {
        await File.WriteAllTextAsync(planned.Path, BuildContent(planned.Content));
        return planned;
    }

    public async Task<IList<K3SClusterResource>> ImportAsync(string id)
    {
        // Id is not K3SClusterResource.Id, it's the "import ID" supplied by Terraform
        // and in this provider, is defined to be the file name.

        if (!File.Exists(id))
        {
            throw new TerraformResourceProviderException($"File '{id}' does not exist.");
        }

        var content = await File.ReadAllTextAsync(id);

        return new[]
        {
            new K3SClusterResource
            {
                Id = Guid.NewGuid().ToString(),
                Path = id,
                Content = content,
            },
        };
    }
}
