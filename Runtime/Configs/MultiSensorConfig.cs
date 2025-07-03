//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace ZeroFramework.Goap
{
    public class MultiSensorConfig : IMultiSensorConfig, IClassCallbackConfig
    {
        public string Name { get; set; }
        public string ClassType { get; set; }
        public Action<object> Callback { get; set; }
    }
}