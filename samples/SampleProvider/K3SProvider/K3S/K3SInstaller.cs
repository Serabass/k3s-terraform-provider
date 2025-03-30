namespace K3SProvider.K3S;

using System;
using System.IO;
using System.Text;
using K3SProvider.Resources;
using Renci.SshNet;

public class K3SInstaller
{
  private readonly string _host;
  private readonly int _port;
  private readonly string _username;
  private readonly string _password;
  private readonly string _sshKey;
  private readonly string? _version;

  private readonly ConnectionInfo _connectionInfo;

  public K3SInstaller(
    string host,
    int port,
    string username,
    string password,
    string sshKey,
    string? version)
  {
    _host = host;
    _port = port;
    _username = username;
    _password = password;
    _sshKey = sshKey;
    _version = version;
    _connectionInfo = new ConnectionInfo(
      _host,
      _port,
      _username,
      new PasswordAuthenticationMethod(_username, _password));
  }

  public K3SInstaller(ServerResource server, string? version)
    : this(
      server.Ssh.Host,
      server.Ssh.Port,
      server.Ssh.Username,
      server.Ssh.Password,
      server.Ssh.SshKey,
      version)
    {
    }

  public K3SInstaller(AgentResource agent, string? version)
    : this(
      agent.Ssh.Host,
      agent.Ssh.Port,
      agent.Ssh.Username,
      agent.Ssh.Password,
      agent.Ssh.SshKey,
      version)
    {
    }

  public string InstallK3SServer()
  {
    using var sshClient = new SshClient(_connectionInfo);
    sshClient.Connect();

    var command = new StringBuilder($"curl -sfL https://get.k3s.io");
    if (_version is not null)
    {
      command.Append($" | INSTALL_K3S_VERSION={_version}+k3s1");
    }
    command.Append($" sh -");
    Logger.Log(command.ToString());
    var result = sshClient.RunCommand(command.ToString());
    return GetK3SServerToken();
  }

  public string InstallK3SAgent(string url, string token)
  {
    using var sshClient = new SshClient(_connectionInfo);
    sshClient.Connect();

    var command = new StringBuilder($"curl -sfL https://get.k3s.io");

    if (_version is not null)
    {
      command.Append($" | INSTALL_K3S_VERSION={_version}");
    }

    command.Append($" K3S_URL={url} ");
    command.Append($" K3S_TOKEN={token} ");
    command.Append($" sh - ");
    Logger.Log(command.ToString());
    var result = sshClient.RunCommand(command.ToString());
    return result.Result;
  }

  public void UninstallK3SServer()
  {
    using var sshClient = new SshClient(_connectionInfo);
    sshClient.Connect();

    var command = "sudo /usr/local/bin/k3s-uninstall.sh";
    Logger.Log(command);
    sshClient.RunCommand(command);
  }

  public void UninstallK3SAgent()
  {
    using var sshClient = new SshClient(_connectionInfo);
    sshClient.Connect();

    var command = "sudo /usr/local/bin/k3s-agent-uninstall.sh";
    Logger.Log(command);
    sshClient.RunCommand(command);
  }

  public string GetK3SServerToken()
  {
    using var sshClient = new SshClient(_connectionInfo);
    sshClient.Connect();

    var command = "sudo cat /var/lib/rancher/k3s/server/token";
    Logger.Log(command);
    var result = sshClient.RunCommand(command);
    return result.Result.Trim();
  }
}
