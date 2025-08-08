//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

#if RABBIT_LOGGER_1
using Keystone.Logger;
using System.Linq;
#else
using System;
using UnityEngine;
#endif
using System.Collections.Generic;

namespace Keystone.Goap.Agent
{
#if RABBIT_LOGGER_1
    public abstract class LoggerBase<TObj> : ILogger<TObj>
        where TObj : class
    {
        protected ILoggerConfig config;
        protected TObj source;
        public abstract string Name { get; }
        public List<string> Logs => this.logger.Logs.Select(x => x.message).ToList();
        private IRabbitLogger logger;

        public void Initialize(ILoggerConfig config, TObj obj)
        {
            this.source = obj;
            this.config = config;

            this.logger = LoggerFactory.Create<TObj>(obj);

            this.UnregisterEvents();
            this.RegisterEvents();
        }

        public void Log(string message) => this.logger.Log(message);
        public void Warning(string message) => this.logger.Warning(message);
        public void Error(string message) => this.logger.Error(message);
        public bool ShouldLog() => this.logger.ShouldLog();

        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();

        ~LoggerBase()
        {
            this.UnregisterEvents();
        }

        public void Dispose()
        {
            this.UnregisterEvents();
            this.logger?.Dispose();
        }
    }

#else
    public abstract class LoggerBase<TObj> : ILogger<TObj>
    {
        protected ILoggerConfig config;
        protected TObj source;
        public abstract string Name { get; }
        public List<string> Logs { get; } = new();

        public void Initialize(ILoggerConfig config, TObj source)
        {
            this.config = config;
            this.source = source;

            UnregisterEvents();
            RegisterEvents();
        }

        public void Log(string message)
        {
#if UNITY_EDITOR
            Handle(message, DebugSeverity.Log);
#endif
        }

        public void Warning(string message)
        {
#if UNITY_EDITOR
            Handle(message, DebugSeverity.Warning);
#endif
        }

        public void Error(string message)
        {
#if UNITY_EDITOR
            Handle(message, DebugSeverity.Error);
#endif
        }

        public bool ShouldLog()
        {
#if UNITY_EDITOR
            return config.DebugMode != DebugMode.None;
#endif

            return false;
        }

        private string FormatLog(string message, DebugSeverity severity)
        {
            var formattedTime = DateTime.Now.ToString("HH:mm:ss");

            return $"<color={GetColor(severity)}>[{formattedTime}]</color>: {message}";
        }

        private string GetColor(DebugSeverity severity)
        {
            switch (severity)
            {
                case DebugSeverity.Log:
                    return "white";
                case DebugSeverity.Warning:
                    return "yellow";
                case DebugSeverity.Error:
                    return "red";
                default:
                    return "white";
            }
        }

        private string FormatConsole(string message)
        {
            return $"{Name}: {message}";
        }

        protected void Handle(string message, DebugSeverity severity)
        {
            switch (config.DebugMode)
            {
                case DebugMode.None:
                    break;
                case DebugMode.Log:
                    Store(FormatLog(message, severity));
                    break;
                case DebugMode.Console:
                    Store(FormatLog(message, severity));
                    AddToConsole(FormatConsole(message), severity);
                    break;
            }
        }

        private void AddToConsole(string message, DebugSeverity severity)
        {
            switch (severity)
            {
                case DebugSeverity.Log:
                    Debug.Log(FormatConsole(message));
                    break;
                case DebugSeverity.Warning:
                    Debug.LogWarning(FormatConsole(message));
                    break;
                case DebugSeverity.Error:
                    Debug.LogError(FormatConsole(message));
                    break;
            }
        }

        private void Store(string message)
        {
            if (config.MaxLogSize == 0)
            {
                return;
            }

            if (Logs.Count >= config.MaxLogSize)
            {
                Logs.RemoveAt(0);
            }

            Logs.Add(message);
        }

        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();

        ~LoggerBase()
        {
            UnregisterEvents();
        }

        public void Dispose()
        {
            UnregisterEvents();
        }
    }
#endif
}