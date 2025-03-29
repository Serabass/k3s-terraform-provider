using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TerraformPluginDotNet.ResourceProvider;

namespace SampleProvider;

public class K3SServerProvider : IResourceProvider<ServerResource>
{
    private readonly SampleConfigurator _configurator;

    public K3SServerProvider(SampleConfigurator configurator)
    {
        _configurator = configurator;
    }

    public Task<ServerResource> PlanAsync(ServerResource? prior, ServerResource proposed)
    {
        return Task.FromResult(proposed);
    }

    public async Task<ServerResource> CreateAsync(ServerResource planned)
    {
        planned.Id = Guid.NewGuid().ToString();
        await File.WriteAllTextAsync(planned.Path, BuildContent(planned.Content));
        return planned;
    }

    public Task DeleteAsync(ServerResource resource)
    {
        File.Delete(resource.Path);
        return Task.CompletedTask;
    }

    public async Task<ServerResource> ReadAsync(ServerResource resource)
    {
        var content = await File.ReadAllTextAsync(resource.Path);
        resource.Content = content;
        return resource;
    }

    public async Task<ServerResource> UpdateAsync(ServerResource? prior, ServerResource planned)
    {
        await File.WriteAllTextAsync(planned.Path, BuildContent(planned.Content));
        return planned;
    }

    public async Task<IList<ServerResource>> ImportAsync(string id)
    {
        // Id is not ServerResource.Id, it's the "import ID" supplied by Terraform
        // and in this provider, is defined to be the file name.

        if (!File.Exists(id))
        {
            throw new TerraformResourceProviderException($"File '{id}' does not exist.");
        }

        var content = await File.ReadAllTextAsync(id);

        return new[]
        {
            new ServerResource
            {
                Id = Guid.NewGuid().ToString(),
                Path = id,
                Content = content,
            },
        };
    }
}
