//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using UnityEngine;

namespace ZeroFramework.Goap.Agent
{
    [Serializable]
    public class LoggerConfig : ILoggerConfig
    {
        [field: SerializeField]
        public DebugMode DebugMode { get; set; } = DebugMode.Log;

        [field: SerializeField]
        public int MaxLogSize { get; set; } = 20;
    }
}
