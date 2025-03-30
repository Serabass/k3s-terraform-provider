namespace K3SProvider;

using System;
using System.IO;

public static class Logger
{
  public static void Log(string message) =>
    File.AppendAllText("debug.log", message + Environment.NewLine);
}
