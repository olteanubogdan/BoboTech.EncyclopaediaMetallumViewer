using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using NLog.Web;
using System;

namespace BoboTech.EncyclopaediaMetallumApiProxy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureNLog();
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel()
                #region NLog configuration, first remove all providers
                .ConfigureLogging(logging => logging.ClearProviders().SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace))
                .UseNLog()
                #endregion
                .Build();

        static void ConfigureNLog()
        {
            var jsonLayout = new JsonLayout { IncludeAllProperties = true };
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "time", Layout = Layout.FromString("${longdate}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "level", Layout = Layout.FromString("${level}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "message", Layout = Layout.FromString("${message}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "appdomain", Layout = Layout.FromString("${appdomain}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "processname", Layout = Layout.FromString("${processname}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "threadid", Layout = Layout.FromString("${threadid}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "logger", Layout = Layout.FromString("${logger}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "exceptionType", Layout = Layout.FromString("${exception:format=Type}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "exceptionMessage", Layout = Layout.FromString("${exception:format=Message}") });
            jsonLayout.Attributes.Add(new JsonAttribute { Name = "exceptionStackTrace", Layout = Layout.FromString("${exception:format=StackTrace}") });
            var config = new LoggingConfiguration { DefaultCultureInfo = System.Globalization.CultureInfo.InvariantCulture };
            var jsonTarget = new FileTarget
            {
                Name = "JsonLog",
                FileName = Layout.FromString("logs/${shortdate}.JsonLog"),
                Layout = jsonLayout
            };
            var fileTarget = new FileTarget
            {
                Name = "FileLog",
                FileName = Layout.FromString("logs/${shortdate}.log"),
                Layout = Layout.FromString("${longdate} ${pad:padding=5:inner=${level}} [${threadid}] ${logger}: ${message}${onexception:inner=${newline}${exception:format=toString}}")
            };
            var consoleTarget = new ConsoleTarget
            {
                DetectConsoleAvailable = true,
                Error = false,
                Name = "ConsoleLog",
                Layout = Layout.FromString("${longdate} ${pad:padding=5:inner=${level}} [${threadid}] ${logger}: ${message}")
            };
            var debuggerTarget = new DebuggerTarget
            {
                Name = "DebuggerLog",
                Layout = Layout.FromString("${longdate} ${processname} ${pad:padding=5:inner=${level}} [${threadid}] ${logger}: ${message}${onexception:inner=${newline}${exception:format=toString}}")
            };
            config.AddTarget(jsonTarget);
            config.AddTarget(fileTarget);
            config.AddTarget(consoleTarget);
            config.AddTarget(debuggerTarget);
            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, jsonTarget.Name);
            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, fileTarget.Name);
            config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, consoleTarget.Name, "BoboTech.EncyclopaediaMetallumApiProxy.*");
            config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, debuggerTarget.Name);
            LogManager.ThrowExceptions = true;
            LogManager.ThrowConfigExceptions = true;
            LogManager.GlobalThreshold = NLog.LogLevel.Trace;
            LogManager.Configuration = config;
        }
    }
}