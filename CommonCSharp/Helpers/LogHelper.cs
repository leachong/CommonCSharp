using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonCSharp.Extend;
using log4net.Repository;
using System.IO;

namespace CommonCSharp.Helpers
{
    public class LogHelper
    {
        const string LOG_PATTERN = "[%date] [T:%thread] [%-5level] [%logger] : %message%newline";
        ILog _log;
        string _identity;
        static string _logDir;

        public LogHelper(Type logType, string identity = "")
        {
            _log = log4net.LogManager.GetLogger(logType);
            _identity = string.IsNullOrEmpty(identity) ? "" : string.Format("[{0}] ", identity);
        }
        public LogHelper(string repositoryName, Type logType, string identity = "")
        {
            _log = log4net.LogManager.GetLogger(repositoryName, logType);
            _identity = string.IsNullOrEmpty(identity) ? "" : string.Format("[{0}] ", identity);
        }
        public static LogHelper Instance { get; } = new LogHelper(typeof(LogHelper));

        public static void LoadAppender(ELogType logType, ELogLevel logLevel, string logPath = "")
        {
            var level = GetLevelByEnum(logLevel);
            if ((logType & ELogType.Console) == ELogType.Console)
            {
                LoadConsoleAppender(null, level);
            }
            if ((logType & ELogType.File) == ELogType.File && !string.IsNullOrEmpty(logPath))
            {
                LoadFileAppender(null, logPath, level);
            }
            if ((logType & ELogType.RollingFile) == ELogType.RollingFile && !string.IsNullOrEmpty(logPath))
            {
                LoadRollingFileAppender(null, logPath, level);
            }
        }
        public static void LoadAppender(object repository, ELogType logType, ELogLevel logLevel, string logPath = "")
        {
            var level = GetLevelByEnum(logLevel);
            if ((logType & ELogType.Console) == ELogType.Console)
            {
                LoadConsoleAppender(repository as ILoggerRepository, level);
            }
            if ((logType & ELogType.File) == ELogType.File && !string.IsNullOrEmpty(logPath))
            {
                LoadFileAppender(repository as ILoggerRepository, logPath, level);
            }
            if ((logType & ELogType.RollingFile) == ELogType.RollingFile && !string.IsNullOrEmpty(logPath))
            {
                LoadRollingFileAppender(repository as ILoggerRepository, logPath, level);
            }
        }
        public static void LoadConsoleAppender(ILoggerRepository repository, Level level)
        {
            var patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = LOG_PATTERN;
            patternLayout.ActivateOptions();


            var consoleAppender = new ColoredConsoleAppender();
            consoleAppender.Threshold = level;
            consoleAppender.Name = "ColoredConsoleAppender";
            consoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Error,
                ForeColor = ColoredConsoleAppender.Colors.Red,
            });
            consoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Warn,
                ForeColor = ColoredConsoleAppender.Colors.Yellow,
            });
            consoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Info,
                ForeColor = ColoredConsoleAppender.Colors.Green,
            });

            consoleAppender.Layout = patternLayout;
            consoleAppender.ActivateOptions();

            if (repository != null)
                BasicConfigurator.Configure(repository, consoleAppender);
            else
                BasicConfigurator.Configure(consoleAppender);
        }
        public static void LoadFileAppender(ILoggerRepository repository, string logPath, Level level)
        {
            var patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = LOG_PATTERN;
            patternLayout.ActivateOptions();

            var fileAppender = new FileAppender();
            fileAppender.Threshold = level;
            fileAppender.Name = "FileAppender";
            fileAppender.File = logPath;
            fileAppender.AppendToFile = false;
            fileAppender.Encoding = Encoding.UTF8;
            fileAppender.Layout = patternLayout;
            fileAppender.ActivateOptions();

            if (repository != null)
                BasicConfigurator.Configure(repository, fileAppender);
            else
                BasicConfigurator.Configure(fileAppender);
        }
        public static void LoadRollingFileAppender(ILoggerRepository repository, string logPath, Level level)
        {
            _logDir = logPath;

            logPath = logPath.AppendFilePathSeparator();
            var patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = LOG_PATTERN;
            patternLayout.ActivateOptions();

            var fileAppender = new RollingFileAppender();
            fileAppender.Threshold = level;
            fileAppender.Name = "RollingFileAppender";
            fileAppender.File = logPath;                                        // 文件名
            fileAppender.MaxSizeRollBackups = -1;                               // 最大变换数量，-1为不限制
            fileAppender.MaximumFileSize = "10MB";                              // 最大文件大小
            fileAppender.StaticLogFileName = false;
            fileAppender.DatePattern = "yyyy-MM-dd'.log'";
            fileAppender.RollingStyle = RollingFileAppender.RollingMode.Date;   // 以日期改变文件名
            fileAppender.AppendToFile = true;                                   // 追加到原文件
            fileAppender.Encoding = Encoding.UTF8;
            fileAppender.Layout = patternLayout;
            fileAppender.ActivateOptions();

            if (repository != null)
                BasicConfigurator.Configure(repository, fileAppender);
            else
                BasicConfigurator.Configure(fileAppender);
        }
        public static void ClearFiles(int d)
        {
            if (!string.IsNullOrEmpty(_logDir) && Directory.Exists(_logDir))
            {
                var logs = Directory.GetFiles(_logDir);
                for (int i = 0; logs != null && i < logs.Length; i++)
                {
                    var logFile = logs[i];
                    var fileName = Path.GetFileNameWithoutExtension(logFile);

                    try
                    {
                        var fileDate = Convert.ToDateTime(fileName);
                        var offset = DateTime.Now - fileDate;
                        if (offset.Days > d)
                        {
                            File.Delete(logFile);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }




        public void d(string format, params object[] args)
        {
            _log.DebugFormat(_identity + format, args);
        }
        public void i(string format, params object[] args)
        {
            _log.InfoFormat(_identity + format, args);
        }
        public void w(string format, params object[] args)
        {
            _log.WarnFormat(_identity + format, args);
        }
        public void e(Exception ex, string format, params object[] args)
        {
            _log.Error(_identity + string.Format(format, args), ex);
        }
        private static Level GetLevelByEnum(ELogLevel level)
        {
            switch (level)
            {
                case ELogLevel.ALL:
                    return Level.All;
                case ELogLevel.DEBUG:
                    return Level.Debug;
                case ELogLevel.INFO:
                    return Level.Info;
                case ELogLevel.WARN:
                    return Level.Warn;
                case ELogLevel.ERROR:
                    return Level.Error;
                case ELogLevel.FATAL:
                    return Level.Fatal;
                default:
                    return Level.All;
            }
        }
        public enum ELogType
        {
            None = 0,
            Console = 1,
            File = 2,
            RollingFile = 4,

            All = Console | File | RollingFile
        }
        public enum ELogLevel
        {
            ALL,
            DEBUG,
            INFO,
            WARN,
            ERROR,
            FATAL,
        }

    }
}
