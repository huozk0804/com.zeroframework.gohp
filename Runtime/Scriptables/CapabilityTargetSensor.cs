//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace ZeroFramework.Goap
{
    [Serializable]
    public class CapabilityTargetSensor : CapabilitySensor
    {
        public ClassRef targetKey = new();

        public override string ToString() => $"{sensor.Name} ({targetKey.Name})";
    }
}