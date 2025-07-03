//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace ZeroFramework.Goap
{
    [Serializable]
    public class CapabilityWorldSensor : CapabilitySensor
    {
        public ClassRef worldKey = new();

        public override string ToString() => $"{sensor.Name} ({worldKey.Name})";
    }
}