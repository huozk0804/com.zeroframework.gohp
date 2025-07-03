//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace ZeroFramework.Goap.Agent
{
    public interface ILogger<TObj> : ILogger
    {
        void Initialize(ILoggerConfig config, TObj obj);
    }

    public interface ILogger : IDisposable
    {
        List<string> Logs { get; }
        void Log(string message);
        void Warning(string message);
        void Error(string message);
        bool ShouldLog();
    }
}
