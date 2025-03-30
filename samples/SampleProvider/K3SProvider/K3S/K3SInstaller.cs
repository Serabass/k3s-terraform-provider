namespace K3SProvider.K3S;

using System;
using System.IO;
using K3SProvider.Resources;
using Renci.SshNet;

public class K3SInstaller
{
  private readonly string _host;
  private readonly int _port;
  private readonly string _username;
  private readonly string _password;
  private readonly string _sshKey;

  private readonly ConnectionInfo _connectionInfo;

  public K3SInstaller(string host, int port, string username, string password, string sshKey)
  {
    _host = host;
    _port = port;
    _username = username;
    _password = password;
    _sshKey = sshKey;

    _connectionInfo = new ConnectionInfo(
      _host,
      _port,
      _username,
      new PasswordAuthenticationMethod(_username, _password));
  }

  public K3SInstaller(ServerResource server)
    : this(server.Host, server.Port, server.Username, server.Password, server.SshKey)
    {
    }

  public K3SInstaller(AgentResource agent)
    : this(agent.Host, agent.Port, agent.Username, agent.Password, agent.SshKey)
    {
    }

  public string InstallK3SServer(string version)
  {
    using var sshClient = new SshClient(_connectionInfo);
    sshClient.Connect();

    var command = $"curl -sfL https://get.k3s.io | INSTALL_K3S_VERSION={version}+k3s1 sh -";
    var result = sshClient.RunCommand(command);
    return GetK3SServerToken();
  }

  public string InstallK3SAgent(string version, string url, string token)
  {
    using var sshClient = new SshClient(_connectionInfo);
    sshClient.Connect();

    var command =
      $"curl -sfL https://get.k3s.io | INSTALL_K3S_VERSION={version}+k3s1 K3S_URL={url} K3S_TOKEN={token} sh -";
    var result = sshClient.RunCommand(command);
    return result.Result;
  }

  public void UninstallK3SServer()
  {
    using var sshClient = new SshClient(_connectionInfo);
    sshClient.Connect();

    var command = "sudo /usr/local/bin/k3s-uninstall.sh";
    sshClient.RunCommand(command);
  }

  public void UninstallK3SAgent()
  {
    using var sshClient = new SshClient(_connectionInfo);
    sshClient.Connect();

    var command = "sudo /usr/local/bin/k3s-agent-uninstall.sh";
    sshClient.RunCommand(command);
  }

  public string GetK3SServerToken()
  {
    using var sshClient = new SshClient(_connectionInfo);
    sshClient.Connect();

    var command = "sudo cat /var/lib/rancher/k3s/server/token";
    var result = sshClient.RunCommand(command);
    return result.Result;
  }
}
