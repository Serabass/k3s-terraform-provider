namespace SampleProvider.K3S;

using System.IO;
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
            new PasswordAuthenticationMethod(_username, _password),
            new PrivateKeyAuthenticationMethod(_sshKey));
    }

    public string InstallK3SServerAsync(string version)
    {
        using var sshClient = new SshClient(_connectionInfo);
        using var sftpClient = new SftpClient(_connectionInfo);
        sshClient.Connect();
        sftpClient.Connect();

        var command = $"curl -sfL https://get.k3s.io | INSTALL_K3S_VERSION={version} sh -";
        var result = sshClient.RunCommand(command);
        var stream = sftpClient.OpenRead("/var/lib/rancher/k3s/server/token");
        var token = new StreamReader(stream).ReadToEnd();
        return token;
    }

    public string InstallK3SAgentAsync(string version, string url, string token)
    {
        using var sshClient = new SshClient(_connectionInfo);
        using var sftpClient = new SftpClient(_connectionInfo);
        sshClient.Connect();
        sftpClient.Connect();

        var command =
            $"curl -sfL https://get.k3s.io | INSTALL_K3S_VERSION={version} K3S_URL={url} K3S_TOKEN={token} sh -";
        var result = sshClient.RunCommand(command);
        return result.Result;
    }

    public void UninstallK3SServerAsync()
    {
        using var sshClient = new SshClient(_connectionInfo);
        sshClient.Connect();

        var command = "sudo /usr/local/bin/k3s-uninstall.sh";
        sshClient.RunCommand(command);
    }

    public void UninstallK3SAgentAsync()
    {
        using var sshClient = new SshClient(_connectionInfo);
        sshClient.Connect();

        var command = "sudo /usr/local/bin/k3s-agent-uninstall.sh";
        sshClient.RunCommand(command);
    }
}
