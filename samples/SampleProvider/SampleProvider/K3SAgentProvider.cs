using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SampleProvider.K3S;
using TerraformPluginDotNet.ResourceProvider;

namespace SampleProvider;

public class K3SAgentProvider : IResourceProvider<AgentResource>
{
    private readonly SampleConfigurator _configurator;

    public K3SAgentProvider(SampleConfigurator configurator)
    {
        _configurator = configurator;
    }

    public Task<AgentResource> PlanAsync(AgentResource? prior, AgentResource proposed)
    {
        return Task.FromResult(proposed);
    }

    public async Task<AgentResource> CreateAsync(AgentResource planned)
    {
        var installer = new K3SInstaller(
            planned.Host,
            planned.Port,
            planned.Username,
            planned.Password,
            planned.SshKey
        );
        installer.InstallK3SAgentAsync(planned.Version, planned.Url, planned.Token);
        return planned;
    }

    public Task DeleteAsync(AgentResource resource)
    {
        File.Delete(resource.Path);
        return Task.CompletedTask;
    }

    public async Task<AgentResource> ReadAsync(AgentResource resource)
    {
        var content = await File.ReadAllTextAsync(resource.Path);
        resource.Content = content;
        return resource;
    }

    public async Task<AgentResource> UpdateAsync(AgentResource? prior, AgentResource planned)
    {
        await File.WriteAllTextAsync(planned.Path, BuildContent(planned.Content));
        return planned;
    }

    public async Task<IList<AgentResource>> ImportAsync(string id)
    {
        // Id is not AgentResource.Id, it's the "import ID" supplied by Terraform
        // and in this provider, is defined to be the file name.

        if (!File.Exists(id))
        {
            throw new TerraformResourceProviderException($"File '{id}' does not exist.");
        }

        var content = await File.ReadAllTextAsync(id);

        return new[]
        {
            new AgentResource
            {
                Id = Guid.NewGuid().ToString(),
                Path = id,
                Content = content,
            },
        };
    }
}
