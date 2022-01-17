using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Infraestructura.Utility
{
  public static class Log
  {
    private static readonly string LOG_CONFIG_FILE = @"log4net.config";

    private static readonly log4net.ILog _log = GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static ILog GetLogger(Type type)
    { return LogManager.GetLogger(type); }

    public static void Error(object mensaje)
    {
      SetLog4NetConfiguration();
      _log.Error(mensaje);
    }

    public static void Error(object mensaje, Exception ex)
    {
      SetLog4NetConfiguration();
      _log.Error(mensaje, ex);
    }

    public static void Informativo(object message)
    {
      SetLog4NetConfiguration();
      _log.Info(message);
    }

    private static void SetLog4NetConfiguration()
    {
      XmlDocument log4netConfig = new XmlDocument();
      log4netConfig.Load(File.OpenRead(LOG_CONFIG_FILE));

      var repo = LogManager.CreateRepository(
          Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

      log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
    }
  }
}
