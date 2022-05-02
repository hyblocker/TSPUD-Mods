using MelonLoader;
using System;

namespace ModThatLetsYouMod
{
    /// <summary>
    /// Internal console, for log levels and stuff
    /// </summary>
    internal class ModConsole
    {
        public static MelonLogger.Instance logger { get; private set; } = new MelonLogger.Instance("TSPUD");
        public static MelonLogger.Instance debugLogger { get; private set; } = new MelonLogger.Instance("TSPUD_DBG");

        private static ConsoleColor ErrorColor = ConsoleColor.Red;
        private static ConsoleColor WarningColor = ConsoleColor.Yellow;

        #region Log
        public static void Log(object obj, LogLevel minLoggingMode = LogLevel.Normal)
        {
            var loggerInstance = minLoggingMode == LogLevel.Debug ? debugLogger : logger;
            if (Bootstrap.config.LogLevel >= minLoggingMode)
                loggerInstance.Msg($"[{minLoggingMode}] {obj}");
        }

        public static void Log(string txt, LogLevel minLoggingMode = LogLevel.Normal)
        {
            var loggerInstance = minLoggingMode == LogLevel.Debug ? debugLogger : logger;
            if (Bootstrap.config.LogLevel >= minLoggingMode)
                loggerInstance.Msg($"[{minLoggingMode}] {txt}");
        }

        public static void Log(string txt, LogLevel minLoggingMode = LogLevel.Normal, params object[] args)
        {
            var loggerInstance = minLoggingMode == LogLevel.Debug ? debugLogger : logger;
            if (Bootstrap.config.LogLevel >= minLoggingMode)
                loggerInstance.Msg($"[{minLoggingMode}] {txt}", args);
        }

        public static void Log(ConsoleColor color, object obj, LogLevel minLoggingMode = LogLevel.Normal)
        {
            var loggerInstance = minLoggingMode == LogLevel.Debug ? debugLogger : logger;
            if (Bootstrap.config.LogLevel >= minLoggingMode)
                loggerInstance.Msg(color, $"[{minLoggingMode}] {obj}");
        }

        public static void Log(ConsoleColor color, string txt, LogLevel minLoggingMode = LogLevel.Normal)
        {
            var loggerInstance = minLoggingMode == LogLevel.Debug ? debugLogger : logger;
            if (Bootstrap.config.LogLevel >= minLoggingMode)
                loggerInstance.Msg(color, $"[{minLoggingMode}] {txt}");
        }

        public static void Log(ConsoleColor color, string txt, LogLevel minLoggingMode = LogLevel.Normal, params object[] args)
        {
            var loggerInstance = minLoggingMode == LogLevel.Debug ? debugLogger : logger;
            if (Bootstrap.config.LogLevel >= minLoggingMode)
                loggerInstance.Msg(color, $"[{minLoggingMode}] {txt}", args);
        }
        #endregion

        #region Warn
        public static void Warn(object obj, LogLevel minLoggingMode = LogLevel.Normal)
        {
            Log(WarningColor, obj, minLoggingMode);
        }

        public static void Warn(string txt, LogLevel minLoggingMode = LogLevel.Normal)
        {
            Log(WarningColor, txt, minLoggingMode);
        }

        public static void Warn(string txt, LogLevel minLoggingMode = LogLevel.Normal, params object[] args)
        {
            Log(WarningColor, txt, minLoggingMode, args);
        }
        #endregion

        #region Error
        public static void Error(object obj, LogLevel minLoggingMode = LogLevel.Normal)
        {
            Log(ErrorColor, obj, minLoggingMode);
        }

        public static void Error(string txt, LogLevel minLoggingMode = LogLevel.Normal)
        {
            Log(ErrorColor, txt, minLoggingMode);
        }

        public static void Error(string txt, LogLevel minLoggingMode = LogLevel.Normal, params object[] args)
        {
            Log(ErrorColor, txt, minLoggingMode, args);
        }
        #endregion
    }

    internal enum LogLevel
    {
        Minimal,
        Normal,
        Verbose,
        Debug,
    }
}
