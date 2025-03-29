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

    public Task<AgentResource> CreateAsync(AgentResource planned)
    {
        var installer = new K3SInstaller(
            planned.Host,
            planned.Port,
            planned.Username,
            planned.Password,
            planned.SshKey);
        installer.InstallK3SAgent(planned.Version, planned.Url, planned.Token);
        return Task.FromResult(planned);
    }

    public Task DeleteAsync(AgentResource resource)
    {
        var installer = new K3SInstaller(
            resource.Host,
            resource.Port,
            resource.Username,
            resource.Password,
            resource.SshKey);
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
